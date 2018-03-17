using System.Collections.Generic;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Services;

namespace coIT.MyHeco.Login.Data.MyHeco
{
    public class MyHecoRepositoryDummy : IMyHecoRepository
    {
        private static IDictionary<string, Benutzer> _users;
        public MyHecoRepositoryDummy()
        {
            if (_users == null)
            {
                _users = new Dictionary<string, Benutzer>(){{"test@myheco.de",new MyHecoBenutzer(new LoginInformation("test@myheco.de","secret"),new Firma("heco"))}};
            }
        }

        public Benutzer FindeBenutzerByMail(string email)
        {
            if(_users.ContainsKey(email))
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

        public void Update(Benutzer benutzer)
        {
            if (_users.ContainsKey(benutzer.Email))
            {
                _users[benutzer.Email] = benutzer;
            }
            else
            {
                _users.Add(benutzer.Email,benutzer);
            }

        }
    }
}