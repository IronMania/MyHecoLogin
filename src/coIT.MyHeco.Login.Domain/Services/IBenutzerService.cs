namespace coIT.MyHeco.Login.Domain.Services
{
    public interface IBenutzerService
    {
        Benutzer Finde(string email);
    }
}