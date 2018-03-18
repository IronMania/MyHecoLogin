using System.Linq;
using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Registrierung.Web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly INichtAktivierteBenutzerRepository _nichtAktivierteBenutzerRepository;

        public AdminController(IUrlHelper urlHelper,
            INichtAktivierteBenutzerRepository nichtAktivierteBenutzerRepository)
        {
            _nichtAktivierteBenutzerRepository = nichtAktivierteBenutzerRepository;
        }

        // GET api/values
        [HttpGet("Users")]
        public IActionResult GetAll()
        {
            var benutzer = _nichtAktivierteBenutzerRepository.All().OfType<NichtAktivierterBenutzer>();
            return Json(benutzer.Select(aktivierterBenutzer => new {email = aktivierterBenutzer.Email, code = aktivierterBenutzer.AktivierungsCode}));
            
        }

    }
}