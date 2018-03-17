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

        public override Benutzer RunPasswortAendern(PasswortAendernParameter arg)
        {
            if (LoginInformation.Passwort.Equals(arg.AltesPasswort) && arg.NeuesPasswort.Equals(arg.NeuesPasswortCheck))
            {
                LoginInformation = LoginInformation.ChangePassword(arg.NeuesPasswort);
            }
            return this;
        }

        public override Benutzer RunLogout()
        {
            return new MyHecoBenutzer(LoginInformation,Firma);
        }
    }
}