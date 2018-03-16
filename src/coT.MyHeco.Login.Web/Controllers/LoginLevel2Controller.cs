using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coT.MyHeco.Login.Web.Controllers
{
    [Route("api/v1/Login")]
    public class LoginLevel2Controller : Controller
    {
        private readonly IBenutzerService _benutzerService;
        private readonly IMyHecoRepository _myHecoRepository;
        private readonly IComWorkRepository _comWorkRepository;

        public LoginLevel2Controller(IMyHecoRepository myHecoRepository, IBenutzerService benutzerService, IComWorkRepository comWorkRepository)
        {
            _myHecoRepository = myHecoRepository;
            _benutzerService = benutzerService;
            _comWorkRepository = comWorkRepository;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string email)
        {
            var benutzer = _benutzerService.Finde(email);
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}")]
        public IActionResult Login(string email, string passwort)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.Login.Run(new LoginParameter(passwort));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}/ManuelleRegistrierung")]
        public IActionResult ManuelleRegistrierung(string email, string firmenName, string passwort)
        {
            Benutzer benutzer =new  UnbekannterBenutzer(new LoginInformation(email,passwort));
            benutzer = benutzer.ManuelleRegistrierung.Run(new ManuelleRegistrierungsParameter(firmenName,passwort));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}/AutoRegistrierung")]
        public IActionResult AutoRegistrierung(string email, string passwort)
        {
            Benutzer benutzer =_comWorkRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.AutomatischeRegistrierung.Run(new AutomatischeRegistrierungsParameter(passwort));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}/Aktivierung")]
        public IActionResult Aktivierung(string email, string aktivierungscode)
        {
            var benutzer =_myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.Aktivieren.Run(new AktivierungsParameter(aktivierungscode));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPost("{email}/Passwort")]
        public IActionResult PasswordZuruecksetzen(string email)
        {
            var benutzer =_myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.PasswortZuruecksetzen.Run();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }
        [HttpPatch("{email}/Passwort")]
        public IActionResult PasswordAendern(string email, string altesPasswort, string neuesPasswort, string neuesPasswortCheck)
        {
            var benutzer =_myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.PasswortAendern.Run(new PasswortAendern(altesPasswort,neuesPasswort,neuesPasswortCheck));
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}/Logout")]
        public IActionResult Ausloggen(string email)
        {
            var benutzer =_myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.Logout.Run();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }
    }
}