using System;

namespace coIT.MyHeco.Login.Domain.Aktionen
{
    public class ManuelleRegistrierungsParameter : AutomatischeRegistrierungsParameter
    {
        public string FirmenName { get; private set; }

        public ManuelleRegistrierungsParameter(string firmenName, string passwort) : base(passwort)
        {
            if (string.IsNullOrEmpty(firmenName))
            {
                throw  new ArgumentException(nameof(firmenName));
            }
            FirmenName = firmenName;
        }
    }
}