using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KIA.HRM.Authorization.ClaimBasedAuthorization.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public CustomAuthorizationAttribute(string? claimToAuthorize) //: base("ClaimBasedAuthorization") فعلا حالت معمولی و ساده می باشد -  یعنی بر حسب پالیسی که بالای هر اکشن تعریف می شود دسترسی چک می شود
        {
            ClaimToAuthorize = claimToAuthorize;
        }
        public string ClaimToAuthorize { get; }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated  
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // You can set the unauthorized response here if needed  
                context.Result = new UnauthorizedResult();
                    return;
            }

            // Check for the required claim  
            if (!context.HttpContext.User.HasClaim(c => c.Type == ClaimToAuthorize))
            {
                // Create a custom response for forbidden access  
                var response = new
                {
                    error = "Access Denied",
                    message = "You do not have permission to access this resource."
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 403 // Set status code to 403 Forbidden  
                };

            }
        }
    }
}
