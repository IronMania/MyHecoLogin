namespace coIT.MyHeco.Login.Domain.Aktionen
{
    public class AutomatischeRegistrierungsParameter
    {
        public string Passwort { get; private set; }

        public AutomatischeRegistrierungsParameter(string passwort)
        {
            Passwort = passwort;
        }
    }
}