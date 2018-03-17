using coIT.MyHeco.Core;
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

        public override Command<Benutzer,AutomatischeRegistrierungsParameter> AutomatischeRegistrierung
            => Command<Benutzer,AutomatischeRegistrierungsParameter>.AlwaysOn(ExecuteRegister);
        public Benutzer ExecuteRegister(AutomatischeRegistrierungsParameter parameter)
        {
            return NichtAktivierterBenutzer.AutoCreate(this,parameter);
        }

    }
}