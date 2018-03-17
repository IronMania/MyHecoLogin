﻿using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Controllers
{
    [Route("api/v1/Login")]
    public class LoginLevel2Controller : Controller
    {

        private readonly IMyHecoRepository _myHecoRepository;

        public LoginLevel2Controller(IMyHecoRepository myHecoRepository)
        {
            _myHecoRepository = myHecoRepository;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return Json(new {status = benutzer.GetType().Name});

            return Redirect($"http://localhost:54187/api/v1/Register/search?email={email}");
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

        [HttpPost("{email}/Passwort")]
        public IActionResult PasswordZuruecksetzen(string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.PasswortZuruecksetzen.Run();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
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
            return Json(new {status = benutzer.GetType().Name});
        }

        [HttpPut("{email}/Logout")]
        public IActionResult Ausloggen(string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            benutzer = benutzer.Logout.Run();
            _myHecoRepository.Update(benutzer);
            _myHecoRepository.Speichern();
            return Json(new {status = benutzer.GetType().Name});
        }
    }
}