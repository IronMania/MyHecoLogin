using System.Collections.Generic;
using System.Threading.Tasks;

namespace coIT.MyHeco.Login.Domain.Services
{
    public interface IRepository
    {
        Benutzer FindeBenutzerByMail(string email);
        IEnumerable<Benutzer> All();
        void Speichern();
    }
}