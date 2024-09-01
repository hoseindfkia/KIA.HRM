using AuthenticationProvider.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims
{
    /// <summary>
    /// تمامی کلیم ولو تمامی کنترلر ها
    /// </summary>
    public static class AllControllersClaimValues
    {
        public static readonly ReadOnlyCollection<(string claimValueEnglish, string claimValuePersian)> AllClaimValues;

        static AllControllersClaimValues()
        {
            var allClaimValues = new List<(string claimValueEnglish, string claimValuePersian)>();
            
            allClaimValues.AddRange(ProjectControllerClaimValues.AllClaimValues);

            // بقیه را اینجا اد میکنیم
            //allClaimValues.AddRange(AdminControllerClaimValues.AllClaimValues);
            //allClaimValues.AddRange(HomeControllerClaimValues.AllClaimValues);

            AllClaimValues = allClaimValues.AsReadOnly();
        }
    }
}
