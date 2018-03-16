using coIT.MyHeco.Login.Domain;
using coT.MyHeco.Login.Web.Extensions;
using coT.MyHeco.Login.Web.Model.Hypermedia;
using Microsoft.AspNetCore.Mvc;

namespace coT.MyHeco.Login.Web.Controllers
{
    public class SirenBenutzerMessageCreater
    {
        private readonly IUrlHelper _urlHelper;

        public SirenBenutzerMessageCreater(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public Siren CreateSirenVonBenutzer(Benutzer user)
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
                    Href = _urlHelper.AbsoluteAction(nameof(LoginController.GenericMethod),"Login",new {email = user.Email,methodName = "Login"})
                };
                submitAction.Fields.Add(new Field("Passwort", Field.InputType.Password));
                result.Actions.Add(submitAction);
            }

            if (user.PasswortZuruecksetzen.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "PasswortZuruecksetzen",
                    Title = "Reset Passwort",
                    Method = Action.MethodType.Post,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginController.GenericMethod),"Login",new {email = user.Email,methodName = "PasswortZuruecksetzen"})
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
                    Method = Action.MethodType.Post,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginController.GenericMethod),"Login",new {email = user.Email,methodName = "PasswortAendern"})
                };
                submitAction.Fields.Add(new Field("AltesPasswort", Field.InputType.Password));
                submitAction.Fields.Add(new Field("NeuesPasswort", Field.InputType.Password));
                submitAction.Fields.Add(new Field("NeuesPasswortCheck", Field.InputType.Password));
                result.Actions.Add(submitAction);
            }

            if (user.Aktivieren.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "Aktivieren",
                    Title = "Account aktivieren",
                    Method = Action.MethodType.Put,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginController.GenericMethod),"Login",new {email = user.Email,methodName = "Aktivieren"})
                };
                submitAction.Fields.Add(new Field("aktivierungscode", Field.InputType.Text));
                result.Actions.Add(submitAction);
            }

            if (user.AutomatischeRegistrierung.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "AutomatischeRegistrierung",
                    Title = "Registrieren",
                    Method = Action.MethodType.Put,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginController.GenericMethod),"Login",new {email = user.Email,methodName = "AutomatischeRegistrierung"})
                };
                submitAction.Fields.Add(new Field("Passwort", Field.InputType.Password));
                result.Actions.Add(submitAction);
            }

            if (user.Logout.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "Logout",
                    Title = "Logout",
                    Method = Action.MethodType.Post,
                    Href = _urlHelper.AbsoluteAction(nameof(LoginController.GenericMethod),"Login",new {email = user.Email,methodName = "Logout"})
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
                case NichtAktivierterBenutzer _:
                    result.Links.Add(new Link(_urlHelper.AbsoluteAction(nameof(AdminController.GetAll), "Admin"),
                        "Adminarea"));
                    break;
                case MyHecoBenutzer myHecoBenutzer:
                    result.Properties.Add("Message",
                        $"Du hast dich {myHecoBenutzer.WrongLogins} mal falsch eingeloggt");
                    break;
            }

            return result;
        }
    }
}