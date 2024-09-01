using AuthenticationProvider.Repositories.Claim;
using AuthenticationProvider.Security.Default;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationProvider.Security.Policy
{
    public static class AuthorizationOptionsStartup
    {
        //
        public static void optios(AuthorizationOptions option)
        {
            option.AddPolicy("Project", policy => policy.RequireClaim(ClaimTypeStore.ProjectList, true.ToString()) // پارامتر دوم مقدار ولیو را چک می کند که فالس یا ترو بود
                                                                                                                   //.RequireClaim(ClaimTypeStore.ProjectSingle) می توانیم چندین کلیم مشخص کنیم                                                     
            );
            option.AddPolicy("CLaimOrPolicy", policy => policy.RequireAssertion(context => Policy.ClaimOrRole(context)));

            option.AddPolicy("ClaimRequierment", policy =>
            policy.Requirements.Add(new ClaimRequirment(ClaimTypeStore.ProjectList)));


        }
    }
}
