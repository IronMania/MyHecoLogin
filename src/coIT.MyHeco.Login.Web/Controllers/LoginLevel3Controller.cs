using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.Services;
using coIT.MyHeco.Login.Web.Extensions;
using coIT.MyHeco.Login.Web.Model.Hypermedia;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Controllers
{
    [Route("api/v2/Login")]
    public class LoginLevel3Controller : Controller
    {
        private readonly SirenBenutzerMessageCreater _benutzerMessageCreater;
        private readonly IMyHecoRepository _myHecoRepository;
        private readonly IUrlHelper _urlHelper;

        public LoginLevel3Controller(IMyHecoRepository myHecoRepository, IUrlHelper urlHelper,
            SirenBenutzerMessageCreater benutzerMessageCreater)
        {
            _myHecoRepository = myHecoRepository;
            _urlHelper = urlHelper;
            _benutzerMessageCreater = benutzerMessageCreater;
        }

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
            return Json(result);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(benutzer));

            return Redirect($"http://localhost:54187/api/v2/Register/search?email={email}");
        }

        [HttpPut("{email}")]
        public IActionResult Login(string email, string passwort)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.Login.Run(new LoginParameter(passwort));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(benutzer));
        }

        [HttpPost("{email}/Passwort")]
        public IActionResult PasswordZuruecksetzen(string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.PasswortZuruecksetzen.Run();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(benutzer));
        }

        [HttpPatch("{email}/Passwort")]
        public IActionResult PasswordAendern(string email, string altesPasswort, string neuesPasswort,
            string neuesPasswortCheck)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.PasswortAendern.Run(new PasswortAendernParameter(altesPasswort, neuesPasswort,
                neuesPasswortCheck));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(benutzer));
        }

        [HttpPut("{email}/Logout")]
        public IActionResult Ausloggen(string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.Logout.Run();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(benutzer));
        }
    }
}