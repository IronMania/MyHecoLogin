using coIT.MyHeco.Registrierung.Domain.Aktionen;
using coIT.MyHeco.Registrierung.Domain.Common;

namespace coIT.MyHeco.Registrierung.Domain
{
    public abstract class Benutzer : AggregateRoot

    {
        public string Email { get; protected set; }

        public virtual Benutzer RunManuelleRegistrierung(
            ManuelleRegistrierungsParameter manuelleRegistrierungsParameter)
        {
            return this;
        }

        public virtual Benutzer RunAutomatischeRegistrierung(
            AutomatischeRegistrierungsParameter automatischeRegistrierungsParameter)
        {
            return this;
        }

        public virtual Benutzer RunAktivieren(AktivierungsParameter aktivierungsParameter)
        {
            return this;
        }
    }
}