using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public class UnbekannterBenutzer : Benutzer
    {
        public LoginInformation LoginInformation { get; }

        public UnbekannterBenutzer(LoginInformation loginInformation)
        {
            LoginInformation = loginInformation;
        }
        public override Command<ManuelleRegistrierungsParameter> ManuelleRegistrierung => Command<ManuelleRegistrierungsParameter>.AlwaysOn(ExecuteRegister);

        private Benutzer ExecuteRegister(ManuelleRegistrierungsParameter loginInformation)
        {
            if (loginInformation.FirmenName.Equals(string.Empty)) return this;
            return NichtAktivierterBenutzer.CreateNew(LoginInformation,new Firma(loginInformation.FirmenName));
        }
    }
}