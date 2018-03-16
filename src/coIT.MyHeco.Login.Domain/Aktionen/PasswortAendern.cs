namespace coIT.MyHeco.Login.Domain.Aktionen
{
    public class PasswortAendern
    {
        public PasswortAendern(string altesPasswort, string neuesPasswort, string neuesPasswortCheck)
        {
            AltesPasswort = altesPasswort;
            NeuesPasswort = neuesPasswort;
            NeuesPasswortCheck = neuesPasswortCheck;
        }

        public string AltesPasswort { get; }
        public string NeuesPasswort { get; }
        public string NeuesPasswortCheck { get; }
    }
}