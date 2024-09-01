using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationProvider.Models.User;
using AuthenticationProvider.Repositories.Claim;
using AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Azure;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization
{
    //مغز متفکر جهت اعمال محدودیت روی هر اکشن متد
    //
    public class ClaimBasedAuthorizationHandler : AuthorizationHandler<ClaimBasedAuthorizationRequirement>
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IClaimBasedAuthorizationUtilities _utilities;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimBasedAuthorizationHandler(SignInManager<UserEntity> signInManager, IClaimBasedAuthorizationUtilities utilities, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _utilities = utilities;
            _httpContextAccessor = httpContextAccessor;
        }



        //protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationRequirement requirement)
        //{
        //    // Your logic to determine if the user is authorized  
        //    bool isAuthorized = false; // your logic to determine if user is authorized;  

        //    if (isAuthorized)
        //    {
        //        context.Succeed(requirement);
        //    }
        //    else
        //    {
        //        context.Fail(); // This will lead to 403 Forbidden response  
        //    }

        //    return Task.CompletedTask;
        //}


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationRequirement requirement)
        {
            // نام کنترلر  و اکشن  را بگیریم
            var claimToAuthorize = _utilities.GetClaimToAuthorize(_httpContextAccessor.HttpContext);
            // کلیمی استفاده شده است یا خیر؟
            if (string.IsNullOrWhiteSpace(claimToAuthorize))
            {
                /// نیاز به احراز هویت ندارد
                /// احراز هویت با موفقیت انجام شده
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            ///برسی این که کاربر ورود انجام داده است یا خیر 
            if (!_signInManager.IsSignedIn(context.User)) return Task.CompletedTask;
            // اگر کاربر کلیمی دارد با کلیم اکسس و کلیم ولو
            // احراز هویت اوکیه
            //     if(context.User.HasClaim(ClaimStore.UserAccess, claimToAuthorize))

            if (context.User.HasClaim(claimToAuthorize, true.ToString()))
            {
                context.Succeed(requirement);
              return Task.CompletedTask;
            }

            var response = new
            {
                error = "Access Denied",
                message = "You do not have permission to access this resource."
            };

           
            //// اگر لاگین کرده و به اینجا رسیده یعنی مجوز نداشته و  باید به 403 فرستاده شود
            /// TODO: اینجا درست عمل نمی کند و به جای خطای 403 ، خطای 404 باز می گرداند.
            /// // نباید 404 بازگرداند چون در فرانت باید بفهمیم مجوز ندارد یا لاگین نکرده است
           // context.Fail();


            /// در صورتی که شرط بالا اعمال نشود احراز هویت انجام نشده است
            return Task.CompletedTask;
        }



        //ChatGPT
        //private readonly ILogger<ClaimBasedAuthorizationHandler> _logger;

        //public ClaimBasedAuthorizationHandler(ILogger<ClaimBasedAuthorizationHandler> logger)
        //{
        //    _logger = logger;
        //}
        //protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationRequirement requirement)
        //{
        //    // Example implementation  
        //    if (context.User.HasClaim(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue))
        //    {
        //        _logger.LogInformation("Claim found. Authorization succeeded.");
        //        context.Succeed(requirement);
        //    }
        //    else
        //    {
        //        _logger.LogWarning("Claim not found. Authorization failed.");
        //    }

        //    return Task.CompletedTask;
        //}
    }
}
