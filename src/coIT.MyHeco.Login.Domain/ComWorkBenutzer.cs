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
        public int Id { get; private set; }
        public override Command<AutomatischeRegistrierungsParameter> AutomatischeRegistrierung
        => Command<AutomatischeRegistrierungsParameter>.AlwaysOn(ExecuteRegister);

        public Firma Firma { get; private set; }

        public Benutzer ExecuteRegister(AutomatischeRegistrierungsParameter parameter)
        {
            return NichtAktivierterBenutzer.AutoCreate(this,parameter);
        }
    }
}