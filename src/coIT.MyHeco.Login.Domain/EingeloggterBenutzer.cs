using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public class EingeloggterBenutzer : MyHecoBenutzer
    {
        public EingeloggterBenutzer(LoginInformation loginInformation, Firma firma) : base(loginInformation,firma)
        {
        }

        public override Command Logout => Command.AlwaysOn(RunLogout);
        public override Command<LoginParameter> Login => Command<LoginParameter>.OffCommand(this);
        public override Command PasswortZuruecksetzen => Command.OffCommand(this);

        public override Command<PasswortAendern> PasswortAendern =>
            Command<PasswortAendern>.AlwaysOn(RunChangePassword);

        private Benutzer RunChangePassword(PasswortAendern arg)
        {
            if (LoginInformation.Passwort.Equals(arg.AltesPasswort) && arg.NeuesPasswort.Equals(arg.NeuesPasswortCheck))
            {
                LoginInformation = LoginInformation.ChangePassword(arg.NeuesPasswort);
            }
            return this;
        }

        private Benutzer RunLogout()
        {
            return new MyHecoBenutzer(LoginInformation,Firma);
        }
    }
}