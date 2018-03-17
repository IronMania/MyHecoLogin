namespace coIT.MyHeco.Core.BenutzerInformationen
{
    public class Firma
    {
        private Firma()
        {
        }

        public Firma(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}