namespace coIT.MyHeco.Registrierung.Domain.Aktionen
{
    public class AktivierungsParameter
    {
        public AktivierungsParameter(string aktivierungscode)
        {
            Aktivierungscode = aktivierungscode;
        }

        public string Aktivierungscode { get; }
    }
}