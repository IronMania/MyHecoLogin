using System;
using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public class MyHecoBenutzer : AggregateRoot
    {
        protected MyHecoBenutzer()
        {
        }

        public MyHecoBenutzer(LoginInformation loginInformation, Firma firma)
        {
            LoginInformation = loginInformation;
            Firma = firma;
        }

        public Firma Firma { get; }

        public LoginInformation LoginInformation { get; protected set; }
        public int WrongLogins { get; private set; }
        public Guid Id { get; private set; }

        public MyHecoBenutzer RunRecoverPassword()
        {
            throw new NotImplementedException();
        }

        public MyHecoBenutzer RunLogin(LoginParameter parameter)
        {
            if (LoginInformation.Passwort.Equals(parameter.Passwort))
                return new EingeloggterBenutzer(LoginInformation, Firma);
            WrongLogins++;
            if (WrongLogins >= 3) return new GesperrterBenutzer(LoginInformation, Firma);
            return this;
        }

        public virtual MyHecoBenutzer RunPasswortZuruecksetzen()
        {
            return this;
        }

        public virtual MyHecoBenutzer RunPasswortAendern(PasswortAendernParameter passwortAendernParameter)
        {
            return this;
        }

        public virtual MyHecoBenutzer RunLogout()
        {
            return this;
        }
    }
}