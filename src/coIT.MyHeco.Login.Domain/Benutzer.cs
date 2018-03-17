using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public abstract class Benutzer : AggregateRoot
    {
        public virtual string Email { get; protected set; }

        public virtual Benutzer RunLogin(LoginParameter loginParameter)
        {
            return this;
        }

        public virtual Benutzer RunManuelleRegistrierung(ManuelleRegistrierungsParameter manuelleRegistrierungsParameter)
        {
            return this;
        }

        public virtual Benutzer RunAutomatischeRegistrierung(AutomatischeRegistrierungsParameter automatischeRegistrierungsParameter)
        {
            return this;
        }

        public virtual Benutzer RunAktivieren(AktivierungsParameter aktivierungsParameter)
        {
            return this;
        }

        public virtual Benutzer RunPasswortZuruecksetzen()
        {

            return this;
        }

        public virtual Benutzer RunPasswortAendern(PasswortAendernParameter passwortAendernParameter)
        {
            return this;
        }

        public virtual Benutzer RunLogout()
        {
            return this;
        }
    }
}