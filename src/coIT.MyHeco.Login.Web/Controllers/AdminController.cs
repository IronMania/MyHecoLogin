using System.Linq;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Services;
using coIT.MyHeco.Login.Web.Extensions;
using coIT.MyHeco.Login.Web.Model.Hypermedia;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IMyHecoRepository _myHecoRepository;
        private readonly IUrlHelper _urlHelper;

        public AdminController(IUrlHelper urlHelper,
            IMyHecoRepository myHecoRepository)
        {
            _urlHelper = urlHelper;
            _myHecoRepository = myHecoRepository;
        }

        // GET api/values
        [HttpGet("Users")]
        public IActionResult GetAll()
        {    
            var result = new Siren(_urlHelper);
            var benutzer = _myHecoRepository.All().OfType<NichtAktivierterBenutzer>()
                .Select(ConvertToSiren);
            result.Entities.AddRange(benutzer);
            return Json(result);
            
        }

        private Siren ConvertToSiren(NichtAktivierterBenutzer user)
        {
            var selfUrl = _urlHelper.AbsoluteAction(nameof(LoginLevel2Controller.Search), "Login", new {email = user.Email});
            var result = new Siren(selfUrl);
            result.Class.Add(user.GetType().Name);
            result.Properties.Add("Email",user.Email);
            result.Properties.Add("ActivationCode",user.AktivierungsCode);
            return result;
        }
    }
}