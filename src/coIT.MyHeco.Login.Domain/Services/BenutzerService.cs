using coIT.MyHeco.Login.Domain.BenutzerInformationen;

namespace coIT.MyHeco.Login.Domain.Services
{
    public class BenutzerService : IBenutzerService
    {
        private readonly IRepository _comworkRepository;
        private readonly IRepository _myHecoRepository;

        public BenutzerService(IMyHecoRepository myHecoRepository, IComWorkRepository comworkRepository)
        {
            _myHecoRepository = myHecoRepository;
            _comworkRepository = comworkRepository;
        }

        public Benutzer Finde(string email)
        {
            var benutzer = _myHecoRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return benutzer;
            benutzer = _comworkRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return benutzer;

            return new UnbekannterBenutzer(new LoginInformation(email,string.Empty));
        }
    }
}