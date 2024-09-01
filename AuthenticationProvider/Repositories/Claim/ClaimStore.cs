using System.Security.Claims;
namespace AuthenticationProvider.Repositories.Claim
{
    public static class ClaimStore
    {
        public const string UserAccess = nameof(UserAccess);

        public static List<System.Security.Claims.Claim> AllClaim = new List<System.Security.Claims.Claim>()
         {
            new System.Security.Claims.Claim(ClaimTypeStore.ProjectList,true.ToString()),
            new System.Security.Claims.Claim(ClaimTypeStore.ProjectSingle,true.ToString()),
            new System.Security.Claims.Claim(ClaimTypeStore.ProjectAdd,true.ToString()),
            new System.Security.Claims.Claim(ClaimTypeStore.ProjectEdit,true.ToString()),
         };
    }

    public static class ClaimTypeStore
    {
        // تایپ ها برای این است که از اشتباه نایپی جلوگیری شود. در این حالت ما بر اساس هر اکشن یک کلیم تعریف می کنیم
        public const string ProjectList = "ProjectList";
        public const string ProjectSingle = "ProjectSingle";
        public const string ProjectAdd = "ProjectAdd";
        public const string ProjectEdit = "ProjectEdit";

    }
}
