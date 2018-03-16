using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public abstract class Benutzer : AggregateRoot

    {
        public virtual string Email { get; protected set; }
        public virtual Command<AutomatischeRegistrierungsParameter> AutomatischeRegistrierung => Command<AutomatischeRegistrierungsParameter>.OffCommand(this);
        public virtual Command<ManuelleRegistrierungsParameter> ManuelleRegistrierung => Command<ManuelleRegistrierungsParameter>.OffCommand(this);
        public virtual Command Logout => Command.OffCommand(this);
        public virtual Command PasswortZuruecksetzen => Command.OffCommand(this);
        public virtual Command<PasswortAendern> PasswortAendern => Command<PasswortAendern>.OffCommand(this);
        public virtual Command<LoginParameter> Login => Command<LoginParameter>.OffCommand(this);
        public virtual Command<AktivierungsParameter> Aktivieren => Command<AktivierungsParameter>.OffCommand(this);
    }
}