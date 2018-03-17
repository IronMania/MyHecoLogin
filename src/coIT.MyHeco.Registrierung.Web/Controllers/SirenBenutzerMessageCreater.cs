using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Web.Controllers;
using coIT.MyHeco.Registrierung.Web.Extensions;
using coIT.MyHeco.Registrierung.Web.Model.Hypermedia;
using Microsoft.AspNetCore.Mvc;

namespace CoIT.MyHeco.Registrierung.Web.Controllers
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
            
            if (user.Aktivieren.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "Aktivieren",
                    Title = "Account aktivieren",
                    Method = Action.MethodType.Post,
                    Href = _urlHelper.AbsoluteAction(nameof(RegisterLevel3Controller.Aktivierung),typeof(RegisterLevel3Controller).ControllerName(),new {email = user.Email})
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
                    Href = _urlHelper.AbsoluteAction(nameof(RegisterLevel3Controller.AutoRegistrierung),typeof(RegisterLevel3Controller).ControllerName(),new {email = user.Email})
                };
                submitAction.Fields.Add(new Field("Passwort", Field.InputType.Password));
                result.Actions.Add(submitAction);
            }

            if (user.ManuelleRegistrierung.CanRun())
            {
                var submitAction = new Action
                {
                    Name = "ManuelleRegistrierung",
                    Title = "Registrieren",
                    Method = Action.MethodType.Put,
                    Href = _urlHelper.AbsoluteAction(nameof(RegisterLevel3Controller.ManuelleRegistrierung),typeof(RegisterLevel3Controller).ControllerName(),new {email = user.Email})
                };
                submitAction.Fields.Add(new Field("Passwort", Field.InputType.Password));
                submitAction.Fields.Add(new Field("Firmenname", Field.InputType.Text));
                result.Actions.Add(submitAction);
            }

            switch (user)
            {
                case NichtAktivierterBenutzer _:
                    result.Links.Add(new Link(_urlHelper.AbsoluteAction(nameof(AdminController.GetAll), "Admin"),
                        "Adminarea"));
                    break;
            }

            return result;
        }
    }
}