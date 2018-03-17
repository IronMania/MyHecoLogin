using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Web.Extensions;
using coIT.MyHeco.Login.Web.Model.Hypermedia;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Controllers
{
    public class SirenBenutzerMessageCreater
    {
        private readonly IUrlHelper _urlHelper;

        public SirenBenutzerMessageCreater(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public Siren CreateSirenVonBenutzer(MyHecoBenutzer user)
        {
            var result = new Siren(_urlHelper);
            result.Class.Add(user.GetType().Name);
            if (user.Login.CanRun())
            {
                
                var submitAction = new Action
                {
                    Name = "Login",
                    Title = "Login",
                    Method = Action.MethodType.Put,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginLevel3Controller.Login),typeof(LoginLevel3Controller).ControllerName(),new {email = user.LoginInformation.Email})
                };
                submitAction.Fields.Add(new Field("Passwort", Field.InputType.Password));
                result.Actions.Add(submitAction);
            }

            if (user.PasswortZuruecksetzen.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "PasswortZuruecksetzen",
                    Title = "Passwort Zurücksetzen",
                    Method = Action.MethodType.Post,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginLevel3Controller.PasswordZuruecksetzen),typeof(LoginLevel3Controller).ControllerName(),new {email = user.LoginInformation.Email})
                };
                submitAction.Fields.Add(new Field("", Field.InputType.Hidden));
                result.Actions.Add(submitAction);
            }

            if (user.PasswortAendern.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "PasswortAendern",
                    Title = "change Passwort",
                    Method = Action.MethodType.Patch,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginLevel3Controller.PasswordAendern),typeof(LoginLevel3Controller).ControllerName(),new {email = user.LoginInformation.Email})
                };
                submitAction.Fields.Add(new Field("AltesPasswort", Field.InputType.Password));
                submitAction.Fields.Add(new Field("NeuesPasswort", Field.InputType.Password));
                submitAction.Fields.Add(new Field("NeuesPasswortCheck", Field.InputType.Password));
                result.Actions.Add(submitAction);
            }

            if (user.Logout.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "Logout",
                    Title = "Logout",
                    Method = Action.MethodType.Put,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginLevel3Controller.Ausloggen),typeof(LoginLevel3Controller).ControllerName(),new {email = user.LoginInformation.Email})
                };
                submitAction.Fields.Add(new Field("", Field.InputType.Hidden));
                result.Actions.Add(submitAction);
            }

            switch (user)
            {
                case EingeloggterBenutzer loggedIn:
                    result.Properties.Add("Message", "Du bist eingeloggt");
                    result.Properties.Add("DeinPasswort",
                        $"Dein geheimes Passwort ist: {loggedIn.LoginInformation.Passwort}");
                    result.Properties.Add("DeineFirma",
                        $"Du arbeitest für: {loggedIn.Firma.Name}");
                    break;
                case GesperrterBenutzer _:
                    result.Properties.Add("Message", "Du hast dich 3 mal falsch eingeloggt und bist gesperrt");
                    break;
                case MyHecoBenutzer myHecoBenutzer:
                    result.Properties.Add("Message",
                        $"Du hast dich {myHecoBenutzer.WrongLogins} mal falsch eingeloggt");
                    result.Links.Add(new Link(_urlHelper.AbsoluteAction(nameof(LoginLevel3Controller.Get)), "Login"));
                    break;
            }

            return result;
        }
    }
}