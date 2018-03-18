namespace coIT.MyHeco.Registrierung.Domain.Services
{
    public class BenutzerSuche
    {
        private readonly IComWorkRepository _comWorkRepository;
        private readonly INichtAktivierteBenutzerRepository _nichtAktivierteBenutzerRepository;

        public BenutzerSuche(IComWorkRepository comWorkRepository,
            INichtAktivierteBenutzerRepository nichtAktivierteBenutzerRepository)
        {
            _comWorkRepository = comWorkRepository;
            _nichtAktivierteBenutzerRepository = nichtAktivierteBenutzerRepository;
        }

        public Benutzer Finde(string email)
        {
            var benutzer = _nichtAktivierteBenutzerRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return benutzer;
            benutzer = _comWorkRepository.FindeBenutzerByMail(email);
            if (benutzer != null) return benutzer;
            return new UnbekannterBenutzer(email);
        }
    }
}