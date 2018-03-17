namespace coIT.MyHeco.Registrierung.Domain.Services
{
    public interface INichtAktivierteBenutzerRepository : IRepository
    {
        void Add(NichtAktivierterBenutzer benutzer);
        void Delete(NichtAktivierterBenutzer benutzer);
        void Speichern();
    }
}