using coIT.MyHeco.Core;
using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Aktionen;

namespace coIT.MyHeco.Login.Domain
{
    public class EingeloggterBenutzer : MyHecoBenutzer
    {
        private EingeloggterBenutzer(){}
        public EingeloggterBenutzer(LoginInformation loginInformation, Firma firma) : base(loginInformation,firma)
        {
        }

        public override Command<MyHecoBenutzer> Logout => Command<MyHecoBenutzer>.AlwaysOn(RunLogout);
        public override Command<MyHecoBenutzer,LoginParameter> Login => Command<MyHecoBenutzer,LoginParameter>.OffCommand(this);
        public override Command<MyHecoBenutzer> PasswortZuruecksetzen => Command<MyHecoBenutzer>.OffCommand(this);

        public override Command<MyHecoBenutzer,PasswortAendernParameter> PasswortAendern =>
            Command<MyHecoBenutzer,PasswortAendernParameter>.AlwaysOn(RunChangePassword);

        private MyHecoBenutzer RunChangePassword(PasswortAendernParameter arg)
        {
            if (LoginInformation.Passwort.Equals(arg.AltesPasswort) && arg.NeuesPasswort.Equals(arg.NeuesPasswortCheck))
            {
                LoginInformation = LoginInformation.ChangePassword(arg.NeuesPasswort);
            }
            return this;
        }

        private MyHecoBenutzer RunLogout()
        {
            return new MyHecoBenutzer(LoginInformation,Firma);
        }
    }
}