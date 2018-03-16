using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Common;
using coIT.MyHeco.Login.Domain.Services;
using coT.MyHeco.Login.Web.Extensions;
using coT.MyHeco.Login.Web.Model.Hypermedia;
using Microsoft.AspNetCore.Mvc;
using Action = coT.MyHeco.Login.Web.Model.Hypermedia.Action;

namespace coT.MyHeco.Login.Web.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IBenutzerService _benutzerService;
        private readonly SirenBenutzerMessageCreater _messageCreater;
        private readonly IMyHecoRepository _myHecoRepository;
        private readonly IUrlHelper _urlHelper;

        public LoginController(IUrlHelper urlHelper, IBenutzerService benutzerService,
            IMyHecoRepository myHecoRepository, SirenBenutzerMessageCreater messageCreater)
        {
            _urlHelper = urlHelper;
            _benutzerService = benutzerService;
            _myHecoRepository = myHecoRepository;
            _messageCreater = messageCreater;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var result = new Siren(_urlHelper);
            var submitAction = new Action
            {
                Name = "search",
                Title = "Login Email",
                Method = Action.MethodType.Get,
                Href = _urlHelper.AbsoluteAction(nameof(Search))
            };
            submitAction.Fields.Add(new Field("email", Field.InputType.Email));
            result.Actions.Add(submitAction);

            result.Links.Add(new Link(_urlHelper.AbsoluteAction(nameof(AdminController.GetAll), "Admin"), "Adminarea"));
            return Json(result);
        }

        [HttpGet("search")]
        public IActionResult Search(string email)
        {
            return RedirectToAction(nameof(GetEmail), new {email});
        }

        [HttpGet("{email}")]
        [HttpPut("{email}")]
        public IActionResult GetEmail(string email)
        {
            var user = _benutzerService.Finde(email);
            var result = _messageCreater.CreateSirenVonBenutzer(user);
            return Json(result);
        }

        [HttpPost("{email}/PasswortZuruecksetzen")]
        public IActionResult ResetPassword(string email)
        {
            return Ok();
        }

        [HttpPut("{email}/{methodName}")]
        [HttpPost("{email}/{methodName}")]
        public IActionResult GenericMethod(string email, string methodName)
        {
            var user = _benutzerService.Finde(email);
            user = ExecuteCommand(methodName, user);
            _myHecoRepository.Update(user);
            return RedirectToAction(nameof(GetEmail), new {email});
        }

        private Benutzer ExecuteCommand(string methodName, Benutzer user)
        {
            var commandInfo = user.GetType().GetProperty(methodName);
            var commandParamter = GetParameter(commandInfo);

            var command = commandInfo.GetValue(user);
            var newuser =
                command.GetType().GetMethod(nameof(Command.Run)).Invoke(command, commandParamter) as Benutzer;
            return newuser;
        }

        private object[] GetParameter(PropertyInfo commandInfo)
        {
            var genericType = commandInfo.PropertyType.GenericTypeArguments.FirstOrDefault();
            if (genericType == null) return new object[0];
            var ctorParmaeters = genericType.GetConstructors().First().GetParameters();
            var parameters = new List<object>();
            foreach (var ctorParmaeter in ctorParmaeters)
            {
                var formvalue = Request.Form[ctorParmaeter.Name].First();
                var parameter = Convert.ChangeType(formvalue, ctorParmaeter.ParameterType);
                parameters.Add(parameter);
            }

            return new[] {Activator.CreateInstance(genericType, parameters.ToArray())};
        }
    }
}