using IdentitySample.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims;
using System.Collections.ObjectModel;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims
{
    public static class ProjectControllerClaimValues
    {
        public const string ProjectList = nameof(ProjectList);
        public const string ProjectListPersian = "لیست پروژه ها";

        public const string ProjectSingle = nameof(ProjectSingle);
        public const string ProjectSinglePersian = "دریافت تکی پروژه";

        //ReadOnlyCollection
        // شبیه به لیست اما پس از مقدار دهی قابل تغییر نیست
        public static readonly ReadOnlyCollection<(string claimValueEnglish, string claimValuePersian)> AllClaimValues;

        static ProjectControllerClaimValues()
        {
            AllClaimValues =
                MvcClaimValuesUtilities.GetPersianAndEnglishClaimValues(typeof(ProjectControllerClaimValues))
                    .ToList()
                    .AsReadOnly();
        }
    }
}
