using coIT.MyHeco.Core;
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

        public override Command<Benutzer,ManuelleRegistrierungsParameter> ManuelleRegistrierung => Command<Benutzer,ManuelleRegistrierungsParameter>.AlwaysOn(ExecuteRegister);

        private Benutzer ExecuteRegister(ManuelleRegistrierungsParameter loginInformation)
        {
            if (loginInformation.FirmenName.Equals(string.Empty)) return this;
            return NichtAktivierterBenutzer.CreateNew(new LoginInformation(Email, loginInformation.Passwort), new Firma(loginInformation.FirmenName));
        }
    }
}