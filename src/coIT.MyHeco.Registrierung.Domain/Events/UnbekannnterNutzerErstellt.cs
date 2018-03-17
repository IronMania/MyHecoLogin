using coIT.MyHeco.Registrierung.Domain.Common;

namespace coIT.MyHeco.Registrierung.Domain.Events
{
    public class UnbekannnterNutzerErstellt : IDomainEvent
    {
        public UnbekannnterNutzerErstellt(string email, string aktivierungscode)
        {
            Email = email;
            Aktivierungscode = aktivierungscode;
        }

        public string Email { get; }
        public string Aktivierungscode { get; }
    }
}