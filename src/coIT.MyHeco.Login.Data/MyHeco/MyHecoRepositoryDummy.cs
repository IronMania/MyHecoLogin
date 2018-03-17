using System.Collections.Generic;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Services;

namespace coIT.MyHeco.Login.Data.MyHeco
{
    public class MyHecoRepositoryDummy : IMyHecoRepository
    {
        private static IDictionary<string, MyHecoBenutzer> _users;

        public MyHecoRepositoryDummy()
        {
            if (_users == null)
                _users = new Dictionary<string, MyHecoBenutzer>
                {
                    {
                        "test@myheco.de",
                        new MyHecoBenutzer(new LoginInformation("test@myheco.de", "secret"), new Firma("heco"))
                    }
                };
        }

        public MyHecoBenutzer FindeBenutzerByMail(string email)
        {
            if (_users.ContainsKey(email))
                return _users[email];
            return null;
        }

        public IEnumerable<MyHecoBenutzer> All()
        {
            return _users.Values;
        }

        public void Speichern()
        {
        }

        public void Update(MyHecoBenutzer benutzer)
        {
            if (_users.ContainsKey(benutzer.LoginInformation.Email))
                _users[benutzer.LoginInformation.Email] = benutzer;
            else
                _users.Add(benutzer.LoginInformation.Email, benutzer);
        }
    }
}