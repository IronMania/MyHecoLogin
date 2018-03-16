using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain.Events
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