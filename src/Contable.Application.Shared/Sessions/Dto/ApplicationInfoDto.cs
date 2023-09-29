using System;
using System.Collections.Generic;

namespace Contable.Sessions.Dto
{
    public class ApplicationInfoDto
    {
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Currency { get; set; }

        public string CurrencySign { get; set; }

        public bool AllowTenantsToChangeEmailSettings { get; set; }

        public bool UserDelegationIsEnabled { get; set; }

        public string LoginKey { get; set; }

        public string LoginToken { get; set; }

        public Dictionary<string, bool> Features { get; set; }

        public List<SessionLevelDto> PrimaryLevels { get; set; }

        public List<SessionLevelDto> SecondaryLevels { get; set; }
    }
}