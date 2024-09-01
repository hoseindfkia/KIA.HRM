using System.Collections.Immutable;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities
{
    public interface IMvcUtilities
    {
        // خاصیت:ImmutableHashSet
        // پس از ایجاد نه می توان حذف کرد و نه می توان ویرایش کرد
        // سرعت بالاتر

        /// <summary>
        /// پس از اجرا نام تمامی اری ها کنترلر ها و اکشن متد ها در این پروپرتی قرار میگیرد و لیستی از آنها را باز می گردااند
        /// به خاطر کند بودن لیست ما از این مدل استفاده کردیم
        /// هش ست یک مدل کالکشن است
        /// </summary>
        public ImmutableHashSet<MvcNamesModel> MvcInfo { get; }

        /// <summary>
        /// فقط اکشن متد هایی که نیاز به احراز هویت کلیم بیس ما دارد
        /// </summary>
        public ImmutableHashSet<MvcNamesModel> MvcInfoForActionsThatRequireClaimBasedAuthorization { get; }
    }
}
