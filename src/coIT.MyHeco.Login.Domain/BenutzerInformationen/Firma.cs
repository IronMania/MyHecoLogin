namespace coIT.MyHeco.Login.Domain.BenutzerInformationen
{
    public class Firma
    {
        public string Name { get; }

        private Firma()
        {
            
        }
        public Firma(string name)
        {
            Name = name;
        }
    }
}