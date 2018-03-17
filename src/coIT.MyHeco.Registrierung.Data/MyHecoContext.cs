using Microsoft.EntityFrameworkCore;

namespace coIT.MyHeco.Registrierung.Data
{
    public class MyHecoContext : DbContext
    {
        public MyHecoContext(DbContextOptions<MyHecoContext> options) : base(options)
        {
        }

        internal DbSet<MyHecoBenutzer> Benutzer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyHecoBenutzer>().Property(tmp => tmp.Passwort)
                .HasColumnName("LoginInformation_Passwort");
            modelBuilder.Entity<MyHecoBenutzer>().Property(tmp => tmp.FirmenName).HasColumnName("Firma_Name");
            modelBuilder.Entity<MyHecoBenutzer>().Property(tmp => tmp.Email).HasColumnName("LoginInformation_Email");
            modelBuilder.Entity<MyHecoBenutzer>().HasKey(tmp => tmp.Id);
        }
    }
}