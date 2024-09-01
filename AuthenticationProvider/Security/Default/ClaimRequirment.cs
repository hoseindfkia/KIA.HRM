using Microsoft.AspNetCore.Authorization;

namespace AuthenticationProvider.Security.Default
{
    public class ClaimRequirment:IAuthorizationRequirement
    {
        public ClaimRequirment(string claimType, string claimValue = null)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
        public string ClaimType { get;  }
        public string ClaimValue { get;  }
    }
}
