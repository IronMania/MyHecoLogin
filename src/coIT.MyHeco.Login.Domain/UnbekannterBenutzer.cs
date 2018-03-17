using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;

namespace coIT.MyHeco.Login.Domain
{
    public class UnbekannterBenutzer : Benutzer
    {
        public UnbekannterBenutzer(LoginInformation loginInformation)
        {
            LoginInformation = loginInformation;
        }

        public LoginInformation LoginInformation { get; }
        public override Benutzer RunManuelleRegistrierung(ManuelleRegistrierungsParameter manuelleRegistrierungsParameter)
        {
            if (manuelleRegistrierungsParameter.FirmenName.Equals(string.Empty)) return this;
            return NichtAktivierterBenutzer.CreateNew(LoginInformation, new Firma(manuelleRegistrierungsParameter.FirmenName));
        }
    }
}