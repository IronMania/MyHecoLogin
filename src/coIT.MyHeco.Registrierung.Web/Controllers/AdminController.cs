using System.Linq;
using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Services;
using coIT.MyHeco.Registrierung.Web.Extensions;
using coIT.MyHeco.Registrierung.Web.Model.Hypermedia;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Registrierung.Web.Controllers
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
            var selfUrl = _urlHelper.AbsoluteAction(nameof(RegisterLevel2Controller.Search), typeof(RegisterLevel2Controller).ControllerName(), new {email = user.Email});
            var result = new Siren(selfUrl);
            result.Class.Add(user.GetType().Name);
            result.Properties.Add("Email",user.Email);
            result.Properties.Add("ActivationCode",user.AktivierungsCode);
            return result;
        }
    }
}