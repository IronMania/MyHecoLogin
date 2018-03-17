using System;

namespace coIT.MyHeco.Registrierung.Domain.Aktionen
{
    public class ManuelleRegistrierungsParameter : AutomatischeRegistrierungsParameter
    {
        public ManuelleRegistrierungsParameter(string firmenName, string passwort) : base(passwort)
        {
            if (string.IsNullOrEmpty(firmenName)) throw new ArgumentException(nameof(firmenName));
            FirmenName = firmenName;
        }

        public string FirmenName { get; }
    }
}