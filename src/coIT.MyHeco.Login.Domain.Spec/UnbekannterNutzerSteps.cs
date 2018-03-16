using System.Linq;
using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Events;
using TechTalk.SpecFlow;
using Xunit;

namespace coIT.MyHeco.Login.Domain.Spec
{
    [Binding]
    public class UnbekannterNutzerSteps
    {
        private Benutzer _benutzer;

        [Given(@"ein unbekannter Nutzter wurde erkannt\.")]
        public void AngenommenEinUnbekannterNutzterWurdeErkannt_()
        {
            _benutzer = new UnbekannterBenutzer(new LoginInformation("new@unbekannt.de",string.Empty));
        }

        [When(@"er all seine Nutzerdaten korrekt eingibt")]
        public void WennErAllSeineNutzerdatenKorrektEingibt()
        {
            _benutzer = _benutzer.ManuelleRegistrierung.Run(new ManuelleRegistrierungsParameter("Firma","secret"));
        }

        [Then(@"Wird ein Event erstellt, das den Sacharbeiter informiert")]
        public void DannWirdEinEventErstelltFurDieVerschickungEinesAktivierungslinks()
        {
            var firstEvent = _benutzer.DomainEvents.First();
            Assert.IsType<UnbekannnterNutzerErstellt>(firstEvent);
        }

        [Then(@"er wird zu einem nicht aktiviertem Benutzer")]
        public void DannErWirdZuEinemNichtAktiviertemNutzer()
        {
            Assert.IsType<NichtAktivierterBenutzer>(_benutzer);
        }
    }
}