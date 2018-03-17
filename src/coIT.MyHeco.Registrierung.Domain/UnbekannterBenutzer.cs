using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Registrierung.Domain.Aktionen;

namespace coIT.MyHeco.Registrierung.Domain
{
    public class UnbekannterBenutzer : Benutzer
    {
        public UnbekannterBenutzer(string email)
        {
            Email = email;
        }

        public override Benutzer RunManuelleRegistrierung(
            ManuelleRegistrierungsParameter manuelleRegistrierungsParameter)
        {
            if (manuelleRegistrierungsParameter.FirmenName.Equals(string.Empty)) return this;
            return NichtAktivierterBenutzer.CreateNew(
                new LoginInformation(Email, manuelleRegistrierungsParameter.Passwort),
                new Firma(manuelleRegistrierungsParameter.FirmenName));
        }
    }
}