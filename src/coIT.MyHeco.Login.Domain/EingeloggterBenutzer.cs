using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Aktionen;

namespace coIT.MyHeco.Login.Domain
{
    public class EingeloggterBenutzer : MyHecoBenutzer
    {
        private EingeloggterBenutzer(){}
        public EingeloggterBenutzer(LoginInformation loginInformation, Firma firma) : base(loginInformation, firma)
        {
        }

        public override MyHecoBenutzer RunPasswortAendern(PasswortAendernParameter arg)
        {
            if (LoginInformation.Passwort.Equals(arg.AltesPasswort) && arg.NeuesPasswort.Equals(arg.NeuesPasswortCheck))
                LoginInformation = LoginInformation.ChangePassword(arg.NeuesPasswort);
            return this;
        }

        public override MyHecoBenutzer RunLogout()
        {
            return new MyHecoBenutzer(LoginInformation, Firma);
        }
    }
}