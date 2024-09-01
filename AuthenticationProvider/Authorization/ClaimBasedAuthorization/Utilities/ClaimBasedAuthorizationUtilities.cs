using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities
{
    public class ClaimBasedAuthorizationUtilities : IClaimBasedAuthorizationUtilities
    {
        private readonly IMvcUtilities _mvcUtilities;

        public ClaimBasedAuthorizationUtilities(IMvcUtilities mvcUtilities)
        {
            _mvcUtilities = mvcUtilities;
        }

        /// <summary>
        /// دریافت تمامی اری ها  و کنترلر ها و اکشن ها
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public string GetClaimToAuthorize(HttpContext httpContext)
        {
           
            var areaName = httpContext.GetRouteValue("area")?.ToString();
            var controllerName = httpContext.GetRouteValue("controller")?.ToString();
            var actionName = httpContext.GetRouteValue("action")?.ToString();

            //var claimToAuthorize = _mvcUtilities.MvcInfoForActionsThatRequireClaimBasedAuthorization
            //    .Where(x => 
            //        x.AreaName == areaName && x.ControllerName == controllerName && x.ActionName == actionName)
            //    .SingleOrDefault();

            ///  به دلیل این که از هش ست استفاده کردیم از مدل پایینی استفاده می کنیم 
            ///   این از هش ست است و جستجو می کند TryGetValue
            _mvcUtilities.MvcInfoForActionsThatRequireClaimBasedAuthorization
                .TryGetValue(new MvcNamesModel(areaName, controllerName, actionName),
                    out var actualValue);

            return actualValue?.ClaimToAuthorize;
        }
    }
}
