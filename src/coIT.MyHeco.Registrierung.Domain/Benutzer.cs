using coIT.MyHeco.Core;
using coIT.MyHeco.Registrierung.Domain.Aktionen;
using coIT.MyHeco.Registrierung.Domain.Common;

namespace coIT.MyHeco.Registrierung.Domain
{
    public abstract class Benutzer : AggregateRoot

    {
        public string Email { get; protected set; }

        public virtual Command<Benutzer,AutomatischeRegistrierungsParameter> AutomatischeRegistrierung => Command<Benutzer,AutomatischeRegistrierungsParameter>.OffCommand(this);
        public virtual Command<Benutzer,ManuelleRegistrierungsParameter> ManuelleRegistrierung => Command<Benutzer,ManuelleRegistrierungsParameter>.OffCommand(this);
        public virtual Command<Benutzer,AktivierungsParameter> Aktivieren => Command<Benutzer,AktivierungsParameter>.OffCommand(this);
    }
}