using coIT.MyHeco.Login.Domain;
using Microsoft.EntityFrameworkCore;

namespace coIT.MyHeco.Login.Data.MyHeco
{
    public class MyHecoDbContext : DbContext
    {
        public MyHecoDbContext(DbContextOptions<MyHecoDbContext> options) : base(options)
        {
        }

        public DbSet<MyHecoBenutzer> Benutzer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyHecoBenutzer>().OwnsOne(tmp => tmp.Firma);
            modelBuilder.Entity<MyHecoBenutzer>().HasKey(tmp => tmp.Id);
            modelBuilder.Entity<MyHecoBenutzer>().OwnsOne(tmp => tmp.LoginInformation);
            modelBuilder.Entity<EingeloggterBenutzer>().HasBaseType<MyHecoBenutzer>();
            modelBuilder.Entity<GesperrterBenutzer>().HasBaseType<MyHecoBenutzer>();
        }
    }
}