using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities;
using AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization
{
    public static class DependencyInjection
    {
        public static void AddClaimBasedAuthorization(this IServiceCollection service)
        {
            service.AddHttpContextAccessor();
            service.AddSingleton<IClaimBasedAuthorizationUtilities, ClaimBasedAuthorizationUtilities>();
            service.AddSingleton<IMvcUtilities, MvcUtilities>();
          //  service.AddSingleton<IAuthorizationHandler, ClaimBasedAuthorizationHandler>();
            // Register the handler  
            service.AddScoped<IAuthorizationHandler, ClaimBasedAuthorizationHandler>();

            service.AddAuthorization(option =>
            {
                option.AddPolicy("ClaimBasedAuthorization", policy =>
                    policy.Requirements.Add(new ClaimBasedAuthorizationRequirement()));
            });
        }
    }
}
