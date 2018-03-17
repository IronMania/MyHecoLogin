using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Aktionen;
using coIT.MyHeco.Registrierung.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Registrierung.Web.Controllers
{
    [Route("api/v1/Register")]
    public class RegisterLevel2Controller : Controller
    {
        private readonly IComWorkRepository _comWorkRepository;
        private readonly INichtAktivierteBenutzerRepository _nichtAktivierteBenutzerRepository;

        public RegisterLevel2Controller(IComWorkRepository comWorkRepository,
            INichtAktivierteBenutzerRepository nichtAktivierteBenutzerRepository)
        {
            _comWorkRepository = comWorkRepository;
            _nichtAktivierteBenutzerRepository = nichtAktivierteBenutzerRepository;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string email)
        {
            var benutzer2 = _nichtAktivierteBenutzerRepository.FindeBenutzerByMail(email);
            if (benutzer2 != null) return Json(new {status = benutzer2.GetType().Name});
            var benutzer3 = _comWorkRepository.FindeBenutzerByMail(email);
            if (benutzer3 != null) return Json(new {status = benutzer3.GetType().Name});
            return Json(new {status = typeof(UnbekannterBenutzer).Name});
        }

        [HttpPut("{email}/ManuelleRegistrierung")]
        public IActionResult ManuelleRegistrierung(string email, string firmenName, string passwort)
        {
            var benutzer = new UnbekannterBenutzer(email);
            var nichtAktivierterBenutzer =
                benutzer.RunManuelleRegistrierung(new ManuelleRegistrierungsParameter(firmenName, passwort));
            _nichtAktivierteBenutzerRepository.Add(nichtAktivierterBenutzer as NichtAktivierterBenutzer);
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(new {status = nichtAktivierterBenutzer.GetType().Name});
        }

        [HttpPut("{email}/AutoRegistrierung")]
        public IActionResult AutoRegistrierung(string email, string passwort)
        {
            var benutzer = _comWorkRepository.FindeBenutzerByMail(email);
            var nichtAktivierterBenutzer =
                benutzer.RunAutomatischeRegistrierung(new AutomatischeRegistrierungsParameter(passwort));
            _nichtAktivierteBenutzerRepository.Add(nichtAktivierterBenutzer as NichtAktivierterBenutzer);
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(new {status = nichtAktivierterBenutzer.GetType().Name});
        }

        [HttpPut("{email}/Aktivierung")]
        public IActionResult Aktivierung(string email, string aktivierungscode)
        {
            var benutzer = _nichtAktivierteBenutzerRepository.FindeBenutzerByMail(email);
            benutzer.RunAktivieren(new AktivierungsParameter(aktivierungscode));
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(new {status = "MyHecoBenutzer"});
        }
    }
}