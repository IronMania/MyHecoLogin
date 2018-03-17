using coIT.MyHeco.Core.BenutzerInformationen;

namespace coIT.MyHeco.Login.Domain
{
    public class GesperrterBenutzer : MyHecoBenutzer
    {
        public GesperrterBenutzer(LoginInformation loginInformation, Firma firma) : base(loginInformation, firma)
        {
        }
    }
}