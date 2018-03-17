namespace coIT.MyHeco.Registrierung.Domain.Aktionen
{
    public class AutomatischeRegistrierungsParameter
    {
        public AutomatischeRegistrierungsParameter(string passwort)
        {
            Passwort = passwort;
        }

        public string Passwort { get; }
    }
}