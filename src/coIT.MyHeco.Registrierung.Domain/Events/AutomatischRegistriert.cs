using coIT.MyHeco.Registrierung.Domain.Common;

namespace coIT.MyHeco.Registrierung.Domain.Events
{
    public class AutomatischRegistriert : IDomainEvent
    {
        public AutomatischRegistriert(string email, string aktivierungscode)
        {
            Email = email;
            Aktivierungscode = aktivierungscode;
        }

        public string Email { get; }
        public string Aktivierungscode { get; }
    }
}