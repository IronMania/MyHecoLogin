using System;
using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Common;
using coIT.MyHeco.Login.Domain.Events;

namespace coIT.MyHeco.Login.Domain
{
    public class NichtAktivierterBenutzer : MyHecoBenutzer
    {
        private NichtAktivierterBenutzer()
        {
        }

        private NichtAktivierterBenutzer(LoginInformation loginInformation, Firma firma) : this(loginInformation,firma,
            Guid.NewGuid().ToString())
        {
        }

        private NichtAktivierterBenutzer(LoginInformation loginInformation, Firma firma, string aktivierungsCode) : base(
            loginInformation,firma)
        {
            AktivierungsCode = aktivierungsCode;
        }
        
        public string AktivierungsCode { get; }

        /// <summary>
        ///     Wenn Registrierung noch geprüft werden soll
        /// </summary>
        /// <param name="loginInformation"></param>
        /// <param name="firma"></param>
        /// <returns></returns>
        public static NichtAktivierterBenutzer CreateNew(LoginInformation loginInformation, Firma firma)
        {
            var result = new NichtAktivierterBenutzer(loginInformation, firma);
            result.AddDomainEvent(new UnbekannnterNutzerErstellt(result.Email,result.AktivierungsCode));
            return result;
        }

        /// <summary>
        ///     Wenn Registrierung Automatisch erfolgen soll
        /// </summary>
        /// <param name="comWorkBenutzer"></param>
        /// <param name="passwort"></param>
        /// <returns></returns>
        public static NichtAktivierterBenutzer AutoCreate(ComWorkBenutzer comWorkBenutzer, AutomatischeRegistrierungsParameter passwort)
        {
            var result = new NichtAktivierterBenutzer(new LoginInformation(comWorkBenutzer.Email,passwort.Passwort),comWorkBenutzer.Firma);
            result.AddDomainEvent(new AutomatischRegistriert(result.Email,result.AktivierungsCode));
            return result;
        }

        public static NichtAktivierterBenutzer VonDatenbank(string email, string aktivierungsCode, string firmenName)
        {
            var result = new NichtAktivierterBenutzer(new LoginInformation(email,string.Empty),new Firma(firmenName), aktivierungsCode);
            return result;
        }

        public override Benutzer RunAktivieren(AktivierungsParameter aktivierungsParameter)
        {
            if (aktivierungsParameter.Aktivierungscode.Equals(AktivierungsCode))
                return new EingeloggterBenutzer(LoginInformation,Firma);

            return this;
        }
    }
}