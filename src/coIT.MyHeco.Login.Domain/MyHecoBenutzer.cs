using System;
using coIT.MyHeco.Core;
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
        public Guid Id { get; protected set; }
        public LoginInformation LoginInformation { get; protected set; }
        public Firma Firma { get; private set; }
        public virtual Command<MyHecoBenutzer,LoginParameter> Login => Command<MyHecoBenutzer,LoginParameter>.AlwaysOn(RunLogin);
        public virtual Command<MyHecoBenutzer> PasswortZuruecksetzen => Command<MyHecoBenutzer>.AlwaysOn(RunRecoverPassword);

        public virtual Command<MyHecoBenutzer> Logout => Command<MyHecoBenutzer>.OffCommand(this);
        public virtual Command<MyHecoBenutzer,PasswortAendernParameter> PasswortAendern => Command<MyHecoBenutzer,PasswortAendernParameter>.OffCommand(this);

        private MyHecoBenutzer RunRecoverPassword()
        {
            throw new NotImplementedException();
        }

        
        public int WrongLogins { get; private set; }
        private MyHecoBenutzer RunLogin(LoginParameter parameter)
        {
            if (LoginInformation.Passwort.Equals(parameter.Passwort)) return new EingeloggterBenutzer(LoginInformation,Firma);
            WrongLogins++;
            if (WrongLogins >= 3) return new GesperrterBenutzer(LoginInformation,Firma);
            return this;
        }
    }
}