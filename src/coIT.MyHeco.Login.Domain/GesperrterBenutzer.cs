using coIT.MyHeco.Core;
using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Aktionen;

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
        public override Command<MyHecoBenutzer,LoginParameter> Login => Command<MyHecoBenutzer,LoginParameter>.OffCommand(this);
        public override Command<MyHecoBenutzer> PasswortZuruecksetzen => Command<MyHecoBenutzer>.OffCommand(this);
    }
}