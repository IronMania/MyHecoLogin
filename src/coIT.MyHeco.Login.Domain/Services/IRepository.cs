using System.Collections.Generic;

namespace coIT.MyHeco.Login.Domain.Services
{
    public interface IRepository
    {
        MyHecoBenutzer FindeBenutzerByMail(string email);
        IEnumerable<MyHecoBenutzer> All();
        void Speichern();
    }
}