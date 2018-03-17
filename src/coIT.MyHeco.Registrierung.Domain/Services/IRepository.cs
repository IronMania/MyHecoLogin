using System.Collections.Generic;

namespace coIT.MyHeco.Registrierung.Domain.Services
{
    public interface IRepository
    {
        Benutzer FindeBenutzerByMail(string email);
        IEnumerable<Benutzer> All();
    }
}