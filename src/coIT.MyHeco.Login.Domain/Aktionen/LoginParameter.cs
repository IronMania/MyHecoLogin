namespace coIT.MyHeco.Login.Domain.Aktionen
{
    public class LoginParameter
    {
        public LoginParameter(string passwort)
        {
            Passwort = passwort;
        }
        public string Passwort { get; set; }
    }
}