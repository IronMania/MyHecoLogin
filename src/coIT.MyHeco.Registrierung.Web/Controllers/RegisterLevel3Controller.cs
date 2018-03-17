using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Aktionen;
using coIT.MyHeco.Registrierung.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoIT.MyHeco.Registrierung.Web.Controllers
{
    [Route("api/v2/Register")]
    public class RegisterLevel3Controller : Controller
    {
        private readonly IComWorkRepository _comWorkRepository;
        private readonly INichtAktivierteBenutzerRepository _nichtAktivierteBenutzerRepository;
        private readonly SirenBenutzerMessageCreater _benutzerMessageCreater;

        public RegisterLevel3Controller(IComWorkRepository comWorkRepository,
            INichtAktivierteBenutzerRepository nichtAktivierteBenutzerRepository, SirenBenutzerMessageCreater benutzerMessageCreater)
        {
            _comWorkRepository = comWorkRepository;
            _nichtAktivierteBenutzerRepository = nichtAktivierteBenutzerRepository;
            _benutzerMessageCreater = benutzerMessageCreater;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string email)
        {
            var benutzer = _nichtAktivierteBenutzerRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(benutzer));
            benutzer = _comWorkRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(benutzer));
            return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(new UnbekannterBenutzer(email)));
        }

        [HttpPut("{email}/ManuelleRegistrierung")]
        public IActionResult ManuelleRegistrierung(string email, string firmenName, string passwort)
        {
            var benutzer = new UnbekannterBenutzer(email);
            var nichtAktivierterBenutzer =
                benutzer.ManuelleRegistrierung.Run(new ManuelleRegistrierungsParameter(firmenName, passwort));
            _nichtAktivierteBenutzerRepository.Add(nichtAktivierterBenutzer as NichtAktivierterBenutzer);
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(nichtAktivierterBenutzer));
        }

        [HttpPut("{email}/AutoRegistrierung")]
        public IActionResult AutoRegistrierung(string email, string passwort)
        {
            var benutzer = _comWorkRepository.FindeBenutzerByMail(email);
            var nichtAktivierterBenutzer =
                benutzer.AutomatischeRegistrierung.Run(new AutomatischeRegistrierungsParameter(passwort));
            _nichtAktivierteBenutzerRepository.Add(nichtAktivierterBenutzer as NichtAktivierterBenutzer);
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(_benutzerMessageCreater.CreateSirenVonBenutzer(nichtAktivierterBenutzer));
        }

        [HttpPost("{email}/Aktivierung")]
        public IActionResult Aktivierung(string email, string aktivierungscode)
        {
            var benutzer = _nichtAktivierteBenutzerRepository.FindeBenutzerByMail(email);
            benutzer.Aktivieren.Run(new AktivierungsParameter(aktivierungscode));
            _nichtAktivierteBenutzerRepository.Speichern();
            return Redirect($"http://localhost:60655/api/v2/Login/search?email={email}");
        }
    }
}