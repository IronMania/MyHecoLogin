using System.Collections.Generic;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Services;

namespace coIT.MyHeco.Login.Data.ComWork
{
    public class ComWorkRepositoryDummy : IComWorkRepository
    {
        private static IDictionary<string, Benutzer> _users;

        public ComWorkRepositoryDummy()
        {
            if (_users == null)
                _users = new Dictionary<string, Benutzer>
                {
                    {"test@comwork.de", new ComWorkBenutzer("test@comwork.de", new Firma("heco"))}
                };
        }

        public Benutzer FindeBenutzerByMail(string email)
        {
            if (_users.ContainsKey(email))
                return _users[email];
            return null;
        }

        public IEnumerable<Benutzer> All()
        {
            return _users.Values;
        }

        public void Speichern()
        {
        }
    }
}