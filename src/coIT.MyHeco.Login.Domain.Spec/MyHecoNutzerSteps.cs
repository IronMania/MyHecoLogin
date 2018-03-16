using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using TechTalk.SpecFlow;
using Xunit;

namespace coIT.MyHeco.Login.Domain.Spec
{
    [Binding]
    public class MyHecoNutzerSteps
    {
        private readonly BenutzerKontext _benutzerKontext;

        public MyHecoNutzerSteps(BenutzerKontext benutzerKontext)
        {
            _benutzerKontext = benutzerKontext;
        }

        [When(@"er das falsche password (.*) mal eingibt")]
        public void WennErDasFalschePasswordMalEingibt(int p0)
        {
            for (var i = 0; i < p0; i++) _benutzerKontext.Benutzer = _benutzerKontext.Benutzer.Login.Run(new LoginParameter("wrongPassword"));
        }


        [Then(@"ist er ein MyHecoBenutzer")]
        public void DannIstErEinMyHecoNutzer()
        {
            Assert.IsType<MyHecoBenutzer>(_benutzerKontext.Benutzer);
        }

        [Then(@"darf sich mithilfe seines Passwortes einloggen")]
        public void DannDarfSichMithilfeSeinesPasswortesEinloggen()
        {
            Assert.True(_benutzerKontext.Benutzer.Login.CanRun());
        }

        [Then(@"darf sein Passwort zurücksetzen\.")]
        public void DannDarfSeinPasswortZurucksetzen_()
        {
            Assert.True(_benutzerKontext.Benutzer.PasswortZuruecksetzen.CanRun());
        }

        [Then(@"ist er ein gesperrter Benutzer")]
        public void DannIstErEinGesperrterNutzer()
        {
            Assert.IsType<GesperrterBenutzer>(_benutzerKontext.Benutzer);
        }

        [Then(@"darf nichts")]
        public void DannDarfNichts()
        {
            Assert.False(_benutzerKontext.Benutzer.PasswortZuruecksetzen.CanRun());
            Assert.False(_benutzerKontext.Benutzer.Logout.CanRun());
            Assert.False(_benutzerKontext.Benutzer.Login.CanRun());
        }

        #region Test1

        [Given(@"ein MyHecoBenutzer wurde erkannt\.")]
        public void AngenommenEinMyHecoNutzerWurdeErkannt()
        {
            _benutzerKontext.Benutzer = new MyHecoBenutzer(new LoginInformation("email@myheco.de", "CorrectPassword"),new Firma("Firma"));
        }


        [When(@"er das richtige Passwort eingibt")]
        public void WennErDasRichtigePasswordEingibt()
        {
            _benutzerKontext.Benutzer = _benutzerKontext.Benutzer.Login.Run(new LoginParameter("CorrectPassword"));
        }

        [Then(@"Ist er ein eingeloggter Benutzer")]
        public void DannIstErEinEingeloggterNutzer()
        {
            Assert.IsType<EingeloggterBenutzer>(_benutzerKontext.Benutzer);
        }

        [Then(@"darf sich ausloggen")]
        public void DannDarfSichAusloggen()
        {
            Assert.True(_benutzerKontext.Benutzer.Logout.CanRun());
        }

        #endregion
    }
}