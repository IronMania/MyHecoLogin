using System.Linq;
using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Events;
using TechTalk.SpecFlow;
using Xunit;

namespace coIT.MyHeco.Login.Domain.Spec
{
    [Binding]
    public class ComworkNutzerSteps
    {
        private Benutzer _benutzer;
        private string _email ="com@work.de" ;

        [Given(@"Angenommen ein ComworkNutzer wurde mittels Email erkannt\.")]
        public void AngenommenAngenommenEinComworkNutzerWurdeMittelsEmailErkannt_()
        {
            _benutzer = new ComWorkBenutzer(_email,new Firma("firma"));
        }

        [When(@"er seinen Account erstellt")]
        public void WennErSeinenAccountErstellt()
        {
            _benutzer = _benutzer.AutomatischeRegistrierung.Run(new AutomatischeRegistrierungsParameter("secret"));
        }

        [Then(@"Wird ein Event erstellt, für die Verschickung eines Aktivierungslinks")]
        public void DannWirdEinEventErstelltFurDieVerschickungEinesAktivierungslinks()
        {
            Assert.IsType<AutomatischRegistriert>(_benutzer.DomainEvents.First());
        }

        [Then(@"ist er ein nicht aktiviertem Nutzer")]
        public void DannIstErEinNichtAktiviertemNutzer()
        {
            Assert.IsType<NichtAktivierterBenutzer>(_benutzer);
        }
    }
}