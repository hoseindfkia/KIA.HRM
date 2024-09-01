using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization.Attributes;


[AttributeUsage(AttributeTargets.Method)]
public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter// AuthorizeAttribute
{
    public CustomAuthorizationAttribute(string? claimToAuthorize) //: base("ClaimBasedAuthorization") فعلا حالت معمولی و ساده می باشد -  یعنی بر حسب پالیسی که بالای هر اکشن تعریف می شود دسترسی چک می شود
    {
        ClaimToAuthorize = claimToAuthorize;
    }
    public string ClaimToAuthorize { get; }


    public  void OnAuthorization(AuthorizationFilterContext context)
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




//[AttributeUsage(AttributeTargets.Method)]
//public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
//{
//    public CustomAuthorizationAttribute(string? claimToAuthorize)
//    {
//        ClaimToAuthorize = claimToAuthorize;
//    }

//    public string ClaimToAuthorize { get; }

//    public void OnAuthorization(AuthorizationFilterContext context)
//    {
//        // Check if the user is authenticated  
//        if (!context.HttpContext.User.Identity.IsAuthenticated)
//        {
//            // You can set the unauthorized response here if needed  
//            context.Result = new UnauthorizedResult();
//            return;
//        }

//        // Check for the required claim  
//        if (!context.HttpContext.User.HasClaim(c => c.Type == ClaimToAuthorize))
//        {
//            // Create a custom response for forbidden access  
//            var response = new
//            {
//                error = "Access Denied",
//                message = "You do not have permission to access this resource."
//            };

//            context.Result = new ObjectResult(response)
//            {
//                StatusCode = 403 // Set status code to 403 Forbidden  
//            };
//        }
//    }
//}



// before
///// <summary>
///// این اتریبیوت روی اکشن متد ها قرار می گیرد 
///// 
///// </summary>
//[AttributeUsage(AttributeTargets.Method)]  // محدود کردن که فقط روی اکشن متد ها بتوانیم استفاده کنیم و نه روی کنترلر ها
//public class CustomAuthorizationAttribute : AuthorizeAttribute
//{
//    public CustomAuthorizationAttribute(string? claimToAuthorize) : base("ClaimBasedAuthorization")// اینجا برای اینه که بتوانیم پالیسی به آن پاس بدهیم base("ClaimBasedAuthorization")
//    {
//        ClaimToAuthorize = claimToAuthorize;
//    }

//    public string ClaimToAuthorize { get; }

//}
