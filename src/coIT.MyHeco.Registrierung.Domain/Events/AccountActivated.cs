using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Registrierung.Domain.Common;

namespace coIT.MyHeco.Registrierung.Domain.Events
{
    public class AccountActivated : IDomainEvent
    {
        public AccountActivated(string passwort, string email, Firma firma)
        {
            Passwort = passwort;
            Email = email;
            Firma = firma;
        }

        public string Passwort { get; }
        public string Email { get; }
        public Firma Firma { get; }
    }
}