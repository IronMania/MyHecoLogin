using System.Linq;
using coIT.MyHeco.Login.Domain;
using Microsoft.EntityFrameworkCore;

namespace coIT.MyHeco.Login.Data.MyHeco
{
    public static class DbInitializer
    {
        public static void Initialize(MyHecoDbContext context)
        {
            context.Database.EnsureCreated();

            var user = context.Benutzer.FirstOrDefault(benutzer =>
                benutzer.LoginInformation.Email.Equals("test@myheco.de"));
            if (user != null) context.Entry(user).State = EntityState.Deleted;
            context.Add(new MyHecoBenutzer(new LoginInformation("test@myheco.de", "secret"), new Firma("heco")));
            context.SaveChanges();
        }
    }
}