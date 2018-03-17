using System;
using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public class MyHecoBenutzer : Benutzer
    {

        protected MyHecoBenutzer()
        {
        }

        public MyHecoBenutzer(LoginInformation loginInformation, Firma firma)
        {
            LoginInformation = loginInformation;
            Firma = firma;
        }

        public override string Email => LoginInformation.Email;
        public Firma Firma { get; private set; }

        private Benutzer RunRecoverPassword()
        {
            throw new NotImplementedException();
        }

        public LoginInformation LoginInformation { get; protected set; }
        public int WrongLogins { get; private set; }
        public override Benutzer RunLogin(LoginParameter parameter)
        {
            if (LoginInformation.Passwort.Equals(parameter.Passwort)) return new EingeloggterBenutzer(LoginInformation,Firma);
            WrongLogins++;
            if (WrongLogins >= 3) return new GesperrterBenutzer(LoginInformation,Firma);
            return this;
        }
    }
}