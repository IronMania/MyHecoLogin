using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Registrierung.Domain.Aktionen;

namespace coIT.MyHeco.Registrierung.Domain
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

        public Firma Firma { get; }

        public override Benutzer RunAutomatischeRegistrierung(AutomatischeRegistrierungsParameter parameter)
        {
            return NichtAktivierterBenutzer.AutoCreate(this, parameter);
        }
    }
}