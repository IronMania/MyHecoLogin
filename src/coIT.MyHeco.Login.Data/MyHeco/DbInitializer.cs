using System.Linq;
using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Login.Domain;
using Microsoft.EntityFrameworkCore;

namespace coIT.MyHeco.Login.Data.MyHeco
{
    public static class DbInitializer
    {
        public static void Initialize(MyHecoDbContext context)
        {
            context.Database.EnsureCreated();
            
            foreach (var myHecoBenutzer in context.Benutzer)
            {

                context.Entry(myHecoBenutzer).State = EntityState.Deleted;
            }

            var benutzer = new MyHecoBenutzer(new LoginInformation("test@myheco.de", "secret"), new Firma("heco"));
            context.Benutzer.Add(benutzer);
            context.SaveChanges();
        }
    }
}