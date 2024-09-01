using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using AuthenticationProvider.Authorization.ClaimBasedAuthorization.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities
{
    public class MvcUtilities : IMvcUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptorCollectionProvider">یک کالکشنی که نام تمامی اری ها و کنترلر ها و اکشن متد ها درون خودش دارد</param>
        public MvcUtilities(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            ///نام تمامی اکشن متد ها به همراه کنترلر و اری ها 
            var mvcInfo = new List<MvcNamesModel>();
            /// نام اکشن متد هایی که نیاز به احراز هویت دارد
            var mvcInfoForActionsThatRequireClaimBasedAuthorization = new List<MvcNamesModel>();

            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (var actionDescriptor in actionDescriptors)
            {
                if (!(actionDescriptor is ControllerActionDescriptor descriptor)) continue;

                var controllerTypeInfo = descriptor.ControllerTypeInfo;

                /// این کد نامی که در اتریبیوت داده ایم را برمی گرداند 
                /// [Authorize(Policy = "ClaimRequirement")]
              // می باشد ClaimRequirement مثال: در کد بالا نامی که بر می گرداند 
                var claimToAuthorize = descriptor.MethodInfo
                    .GetCustomAttribute<CustomAuthorizationAttribute>()?.ClaimToAuthorize;

               
                mvcInfo.Add(new MvcNamesModel(
                    controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    descriptor.ControllerName,
                    descriptor.ActionName,
                    claimToAuthorize));

                /// اینجا مشخص می شود احراز هویت هایی که پالیسی خالی نیست در اینجا پر شود
                /// : به عبارتی این اتریبیوت زمانی پر می شود که روی اکشن کلیم تعریف کرده باشیم. مثال
                ///  [Authorize(Policy = "ClaimRequirement")]
                if (!string.IsNullOrWhiteSpace(claimToAuthorize))
                    mvcInfoForActionsThatRequireClaimBasedAuthorization.Add(new MvcNamesModel(
                        controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                        descriptor.ControllerName,
                        descriptor.ActionName,
                        claimToAuthorize));
            }
            ///تمامی نام ها گرفته شد و درون این دو اتریبیوت قرار گرفت
            MvcInfo = ImmutableHashSet.CreateRange(mvcInfo);
            MvcInfoForActionsThatRequireClaimBasedAuthorization = 
                ImmutableHashSet.CreateRange(mvcInfoForActionsThatRequireClaimBasedAuthorization);
        }

        /// <summary>
        /// پس از اجرای سازنده درون این دو پر می شود
        /// </summary>
        public ImmutableHashSet<MvcNamesModel> MvcInfo { get; }
        public ImmutableHashSet<MvcNamesModel> MvcInfoForActionsThatRequireClaimBasedAuthorization { get; }
    }
}
