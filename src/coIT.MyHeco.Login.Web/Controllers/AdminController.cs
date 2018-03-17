using System.Linq;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Services;
using coIT.MyHeco.Login.Web.Extensions;
using coIT.MyHeco.Login.Web.Model.Hypermedia;
using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly INichtAktivierteBenutzerRepository _nichtAktivierteBenutzerRepository;
        private readonly IUrlHelper _urlHelper;

        public AdminController(IUrlHelper urlHelper,
            INichtAktivierteBenutzerRepository nichtAktivierteBenutzerRepository)
        {
            _urlHelper = urlHelper;
            _nichtAktivierteBenutzerRepository = nichtAktivierteBenutzerRepository;
        }

        // GET api/values
        [HttpGet("Users")]
        public IActionResult GetAll()
        {    
            var result = new Siren(_urlHelper);
            var benutzer = _nichtAktivierteBenutzerRepository.All().OfType<NichtAktivierterBenutzer>()
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