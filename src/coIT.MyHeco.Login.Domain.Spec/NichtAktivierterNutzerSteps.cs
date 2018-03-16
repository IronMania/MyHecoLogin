using coIT.MyHeco.Login.Domain.Aktionen;
using TechTalk.SpecFlow;
using Xunit;

namespace coIT.MyHeco.Login.Domain.Spec
{
    [Binding]
    public class NichtAktivierterNutzerSteps
    {
        private readonly string _aktivierungsCode = "AktivierungsCode";
        private Benutzer _benutzer;

        [Given(@"ein nicht aktivierter Nutzer wurde erkannt\.")]
        public void AngenommenEinNichtAktivierterNutzerWurdeErkannt()
        {
            _benutzer = NichtAktivierterBenutzer.VonDatenbank("email", _aktivierungsCode,"firma");
        }

        [When(@"er seinen Account aktiviert und ein Password eingibt\.")]
        public void WennErSeinenAccountAktiviertUndEinPasswordEingibt()
        {
            _benutzer = _benutzer.Aktivieren.Run(new AktivierungsParameter(_aktivierungsCode));
        }

        [When(@"er seinen Account mit dem falschen Code aktiviert und ein Password eingibt\.")]
        public void WennErSeinenAccountMitDemFalschenCodeAktiviertUndEinPasswordEingibt()
        {
            _benutzer = _benutzer.Aktivieren.Run(new AktivierungsParameter("falscherCode"));
        }

        [Then(@"Ist er ein eingeloggter Nutzer\.")]
        public void DannIstErEinEingeloggterNutzer()
        {
            Assert.IsType<EingeloggterBenutzer>(_benutzer);
        }

        [Then(@"ist er ein nicht aktiviertem Nutzer\.")]
        public void DannIstErEinNichtAktiviertemNutzer()
        {
            Assert.IsType<NichtAktivierterBenutzer>(_benutzer);
        }
    }
}