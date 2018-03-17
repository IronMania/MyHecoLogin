namespace coIT.MyHeco.Login.Domain.Services
{
    public interface IMyHecoRepository : IRepository
    {
        void Update(MyHecoBenutzer benutzer);
    }
}