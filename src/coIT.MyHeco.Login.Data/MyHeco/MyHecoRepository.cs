using System.Collections.Generic;
using System.Linq;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace coIT.MyHeco.Login.Data.MyHeco
{
    public class MyHecoRepository : IMyHecoRepository
    {
        private readonly MyHecoDbContext _context;

        public MyHecoRepository(MyHecoDbContext context)
        {
            _context = context;
        }

        public MyHecoBenutzer FindeBenutzerByMail(string email)
        {
            return _context.Benutzer.AsNoTracking()
                .FirstOrDefault(benutzer => benutzer.LoginInformation.Email.Equals(email));
        }

        public IEnumerable<MyHecoBenutzer> All()
        {
            return _context.Benutzer;
        }

        public void Speichern()
        {
            _context.SaveChanges();
        }

        public void Update(MyHecoBenutzer benutzer)
        {
            var user = _context.Benutzer.First(hecoBenutzer =>
                hecoBenutzer.LoginInformation.Email.Equals(benutzer.LoginInformation.Email));
            _context.Entry(user).State = EntityState.Deleted;
            _context.Add(benutzer);
        }
    }
}