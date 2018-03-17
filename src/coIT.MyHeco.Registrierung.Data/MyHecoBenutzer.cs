using System;

namespace coIT.MyHeco.Registrierung.Data
{
    internal class MyHecoBenutzer
    {
        public Guid Id { get; set; }
        public string Passwort { get; set; }
        public string FirmenName { get; set; }
        public string Email { get; set; }
    }
}