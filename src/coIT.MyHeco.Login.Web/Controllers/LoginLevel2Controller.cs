using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.Services;
using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Aktionen;
using coIT.MyHeco.Registrierung.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Controllers
{
    [Route("api/v1/Login")]
    public class LoginLevel2Controller : Controller
    {
        private readonly IComWorkRepository _comWorkRepository;
        private readonly IMyHecoRepository _myHecoRepository;
        private readonly INichtAktivierteBenutzerRepository _nichtAktivierteBenutzerRepository;

        public LoginLevel2Controller(IMyHecoRepository myHecoRepository, IComWorkRepository comWorkRepository,
            INichtAktivierteBenutzerRepository nichtAktivierteBenutzerRepository)
        {
            _myHecoRepository = myHecoRepository;
            _comWorkRepository = comWorkRepository;
            _nichtAktivierteBenutzerRepository = nichtAktivierteBenutzerRepository;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return Json(new {status = benutzer.GetType().Name});

            var benutzer2 = _nichtAktivierteBenutzerRepository.FindeBenutzerByMail(email);
            if (benutzer2 != null) return Json(new {status = benutzer2.GetType().Name});
            var benutzer3 = _comWorkRepository.FindeBenutzerByMail(email);
            if (benutzer3 != null) return Json(new {status = benutzer3.GetType().Name});
            return Json(new {status = typeof(UnbekannterBenutzer).Name});
        }

        [HttpPut("{email}")]
        public IActionResult Login(string email, string passwort)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.RunLogin(new LoginParameter(passwort));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}/ManuelleRegistrierung")]
        public IActionResult ManuelleRegistrierung(string email, string firmenName, string passwort)
        {
            var benutzer = new UnbekannterBenutzer(email);
            var nichtAktivierterBenutzer =
                benutzer.RunManuelleRegistrierung(new ManuelleRegistrierungsParameter(firmenName, passwort)) as
                    NichtAktivierterBenutzer;
            _nichtAktivierteBenutzerRepository.Add(nichtAktivierterBenutzer);
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(new {status = _nichtAktivierteBenutzerRepository.GetType().Name});
        }

        [HttpPut("{email}/AutoRegistrierung")]
        public IActionResult AutoRegistrierung(string email, string passwort)
        {
            var benutzer = _comWorkRepository.FindeBenutzerByMail(email);
            var nichtAktivierterBenutzer =
                benutzer.RunAutomatischeRegistrierung(new AutomatischeRegistrierungsParameter(passwort)) as
                    NichtAktivierterBenutzer;
            _nichtAktivierteBenutzerRepository.Add(nichtAktivierterBenutzer);
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(new {status = _nichtAktivierteBenutzerRepository.GetType().Name});
        }

        [HttpPut("{email}/Aktivierung")]
        public IActionResult Aktivierung(string email, string aktivierungscode)
        {
            var benutzer = _nichtAktivierteBenutzerRepository.FindeBenutzerByMail(email);
            benutzer.RunAktivieren(new AktivierungsParameter(aktivierungscode));
            _nichtAktivierteBenutzerRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPost("{email}/Passwort")]
        public IActionResult PasswordZuruecksetzen(string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.RunPasswortZuruecksetzen();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPatch("{email}/Passwort")]
        public IActionResult PasswordAendern(string email, string altesPasswort, string neuesPasswort,
            string neuesPasswortCheck)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.RunPasswortAendern(new PasswortAendernParameter(altesPasswort, neuesPasswort,
                neuesPasswortCheck));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}/Logout")]
        public IActionResult Ausloggen(string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.RunLogout();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }
    }
}