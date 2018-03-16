namespace coIT.MyHeco.Login.Domain.Aktionen
{
    public class AktivierungsParameter
    {
        public AktivierungsParameter(string aktivierungscode)
        {
            Aktivierungscode = aktivierungscode;
        }

        public string Aktivierungscode { get; private set; }
    }
}