using System;
using coIT.MyHeco.Core;
using coIT.MyHeco.Core.BenutzerInformationen;
using coIT.MyHeco.Registrierung.Domain.Aktionen;
using coIT.MyHeco.Registrierung.Domain.Events;

namespace coIT.MyHeco.Registrierung.Domain
{
    public class NichtAktivierterBenutzer : Benutzer
    {
        private NichtAktivierterBenutzer()
        {
        }

        private NichtAktivierterBenutzer(LoginInformation loginInformation, Firma firma) : this(loginInformation, firma,
            Guid.NewGuid().ToString())
        {
        }

        private NichtAktivierterBenutzer(LoginInformation loginInformation, Firma firma, string aktivierungsCode)
        {
            AktivierungsCode = aktivierungsCode;
            Firma = firma;
            Passwort = loginInformation.Passwort;
            Email = loginInformation.Email;
        }

        public string Passwort { get; }

        public string AktivierungsCode { get; }
        public Firma Firma { get; }

        /// <summary>
        ///     Wenn Registrierung noch geprüft werden soll
        /// </summary>
        /// <param name="loginInformation"></param>
        /// <param name="firma"></param>
        /// <returns></returns>
        public static NichtAktivierterBenutzer CreateNew(LoginInformation loginInformation, Firma firma)
        {
            var result = new NichtAktivierterBenutzer(loginInformation, firma);
            result.AddDomainEvent(new UnbekannnterNutzerErstellt(result.Email, result.AktivierungsCode));
            return result;
        }

        /// <summary>
        ///     Wenn Registrierung Automatisch erfolgen soll
        /// </summary>
        /// <param name="comWorkBenutzer"></param>
        /// <param name="passwort"></param>
        /// <returns></returns>
        public static NichtAktivierterBenutzer AutoCreate(ComWorkBenutzer comWorkBenutzer,
            AutomatischeRegistrierungsParameter passwort)
        {
            var result = new NichtAktivierterBenutzer(new LoginInformation(comWorkBenutzer.Email, passwort.Passwort),
                comWorkBenutzer.Firma);
            result.AddDomainEvent(new AutomatischRegistriert(result.Email, result.AktivierungsCode));
            return result;
        }

        public static NichtAktivierterBenutzer VonDatenbank(string email, string aktivierungsCode, string firmenName)
        {
            var result = new NichtAktivierterBenutzer(new LoginInformation(email, string.Empty), new Firma(firmenName),
                aktivierungsCode);
            return result;
        }

        public override Command<Benutzer,AktivierungsParameter> Aktivieren => Command<Benutzer,AktivierungsParameter>.AlwaysOn(RunAktivieren);
        private Benutzer RunAktivieren(AktivierungsParameter aktivierungsParameter)
        {
            if (aktivierungsParameter.Aktivierungscode.Equals(AktivierungsCode))
            {
                AddDomainEvent(new AccountActivated(Passwort,Email,Firma));
            }
            return this;
        }
    }
}