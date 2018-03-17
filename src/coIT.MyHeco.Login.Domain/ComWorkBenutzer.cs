using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public class ComWorkBenutzer : Benutzer
    {
        private ComWorkBenutzer()
        {
        }

        public ComWorkBenutzer(string email, Firma firma)
        {
            Email = email;
            Firma = firma;
        }
        
        public Firma Firma { get; private set; }

        public override Benutzer RunAutomatischeRegistrierung(AutomatischeRegistrierungsParameter parameter)
        {
            return NichtAktivierterBenutzer.AutoCreate(this,parameter);
        }
    }
}