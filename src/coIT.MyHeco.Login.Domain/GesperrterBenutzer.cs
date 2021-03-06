﻿using coIT.MyHeco.Login.Domain.Aktionen;
using coIT.MyHeco.Login.Domain.BenutzerInformationen;
using coIT.MyHeco.Login.Domain.Common;

namespace coIT.MyHeco.Login.Domain
{
    public class GesperrterBenutzer : MyHecoBenutzer
    {
        public GesperrterBenutzer(LoginInformation loginInformation, Firma firma) : base(loginInformation, firma)
        {
        }
    }
}