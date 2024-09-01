using AuthenticationProvider.Repositories.Claim;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationProvider.Security.Policy
{
    public static class Policy
    {
        /// <summary>
        /// یا ادمین باشد یا پالیسی لیست پروژه را داشته باشد
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool ClaimOrRole(AuthorizationHandlerContext context)
        {
            return context.User.HasClaim(ClaimTypeStore.ProjectList, true.ToString()) ||
                                                      context.User.IsInRole("Admin"); // در اینجا دو تا شرط چک شده یکی این که کلیم لیست پروژه را داشته باشد و یا ادمین باشد
        }

    }


}
