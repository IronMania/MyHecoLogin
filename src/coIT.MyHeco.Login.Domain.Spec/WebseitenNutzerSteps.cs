using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Services;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace coIT.MyHeco.Login.Domain.Spec
{
    [Binding]
    public class WebseitenNutzerSteps
    {
        private readonly string _comWorkBekannt = "comwork@bekannt.de";
        private readonly BenutzerKontext _kontext;
        private readonly string _myHecoBekannt = "myHeco@bekannt.de";
        private readonly string _myHecoNichtAktiviert = "myHeco@aktivieren.de";
        private Mock<IComWorkRepository> _comWorkRepository;
        private Mock<IMyHecoRepository> _myHecoRepository;

        private BenutzerService _service;

        public WebseitenNutzerSteps(BenutzerKontext kontext)
        {
            _kontext = kontext;
        }

        [Given(@"ein nicht angemeldeter Benutzer kommt auf die Webseite\.")]
        public void AngenommenEinNichtAngemeldeterBenutzerKommtAufDieWebseite_()
        {
            _myHecoRepository = new Mock<IMyHecoRepository>();
            _myHecoRepository.Setup(repository => repository.FindeBenutzerByMail(_myHecoBekannt))
                .Returns(new MyHecoBenutzer(new LoginInformation(_myHecoBekannt, "secret"),new Firma("firma")));
            _myHecoRepository.Setup(repository => repository.FindeBenutzerByMail(_myHecoNichtAktiviert))
                .Returns(NichtAktivierterBenutzer.VonDatenbank(_myHecoNichtAktiviert, "unimportantCode","firma"));
            _comWorkRepository = new Mock<IComWorkRepository>();
            _comWorkRepository.Setup(repository => repository.FindeBenutzerByMail(_comWorkBekannt))
                .Returns(new ComWorkBenutzer(_comWorkBekannt,new Firma("firma")));
            _service = new BenutzerService(_myHecoRepository.Object, _comWorkRepository.Object);
        }

        [When(@"er eine Email eingibt, die das System nicht kennt")]
        public void WennErEineEmailEingibtDieDasSystemNichtKennt()
        {
            _kontext.Benutzer = _service.Finde("un@bekannt.de");
        }

        [When(@"er eine Email eingibt, die im Comwork hinterlegt ist")]
        public void WennErEineEmailEingibtDieImComworkHinterlegtIst()
        {
            _kontext.Benutzer = _service.Finde(_comWorkBekannt);
        }

        [When(@"er eine Email eingibt, die im MyHeco hinterlegt ist")]
        public void WennErEineEmailEingibtDieImMyHecoHinterlegtIst()
        {
            _kontext.Benutzer = _service.Finde(_myHecoBekannt);
        }

        [Then(@"wird er zu einem unbekannten Nutzer")]
        public void DannWirdErZuEinemUnbekanntenNutzer()
        {
            Assert.IsType<UnbekannterBenutzer>(_kontext.Benutzer);
        }

        [Then(@"wird er zu einem ComworkNutzer")]
        public void DannWirdErZuEinemComworkNutzer()
        {
            Assert.IsType<ComWorkBenutzer>(_kontext.Benutzer);
        }

        [Then(@"ist er ein MyHecoNutzer")]
        public void DannIstErEinMyHecoNutzer()
        {
            Assert.IsType<MyHecoBenutzer>(_kontext.Benutzer);
        }
    }
}