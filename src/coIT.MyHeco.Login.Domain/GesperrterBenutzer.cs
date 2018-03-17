using coIT.MyHeco.Core.BenutzerInformationen;

namespace coIT.MyHeco.Login.Domain
{
    public class GesperrterBenutzer : MyHecoBenutzer
    {
        private GesperrterBenutzer()
        {
            
        }
        public GesperrterBenutzer(LoginInformation loginInformation, Firma firma) : base(loginInformation, firma)
        {
        }
    }
}