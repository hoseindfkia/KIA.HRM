using System.ComponentModel;


namespace Share.Enum
{
    /// <summary>
    /// جهت تعیین وضعیت درخواست اطلاعات از سرور یا پایگاه داده
    /// </summary>
    public enum FeedbackStatus
    {
        /// <summary>
        /// امکان ارتباط با سرور میسر نمی باشد
        /// </summary>
        [Description("امکان ارتباط با سرور میسر نمی باشد.")]
        CouldNotConnectToServer = 0,
        /// <summary>
        /// امکان ارتباط با پایگاه داده میسر نمی باشد
        /// </summary>
        [Description("امکان ارتباط با پایگاه داده میسر نمی باشد.")]
        CouldNotConnectToDataBase = 1,
        /// <summary>
        /// اطلاعات مورد نظر پیدا نشده است
        /// </summary>
        [Description("اطلاعات مورد نظر پیدا نشده است.")]
        DataIsNotFound = 2,
        /// <summary>
        /// فایل مورد نظر پیدا نشده است
        /// </summary>
        [Description("فایل مورد نظر پیدا نشده است.")]
        FileIsNotFound = 3,
        /// <summary>
        /// فرمت داده ی وارد شده اشتباه می باشد
        /// </summary>
        [Description("فرمت داده ی وارد شده اشتباه می باشد.")]
        InvalidDataFormat = 4,
        /// <summary>
        /// دریافت اطلاعات با موفقیت انجام شد
        /// </summary>
        [Description("دریافت اطلاعات با موفقیت انجام شد.")]
        FetchSuccessful = 5,
        /// <summary>
        /// اطلاعات با موفقیت ثبت شد
        /// </summary>
        [Description("اطلاعات با موفقیت ثبت شد.")]
        RegisteredSuccessful = 6,
        /// <summary>
        /// اطلاعات با موفقیت ویرایش شد
        /// </summary>
        [Description("اطلاعات با موفقیت ویرایش شد.")]
        UpdatedSuccessful = 7,
        /// <summary>
        /// اطلاعات با موفقیت حذف شد
        /// </summary>
        [Description("اطلاعات با موفقیت حذف شد.")]
        DeletedSuccessful = 8,
        /// <summary>
        /// فایل مورد نظر معتبر نیست
        /// </summary>
        [Description("فایل مورد نظر معتبر نیست.")]
        FileIsNotValid = 9,
        /// <summary>
        /// اندازه فایل مورد نظر بیشتر از حداکثر اندازه تعیین شده می باشد
        /// </summary>
        [Description("اندازه فایل مورد نظر بیشتر از حداکثر اندازه تعیین شده می باشد")]
        FileSizeIsMoreThanMaximumSize = 10,
        /// <summary>
        /// اجازه دسترسی به بخش مورد نظر وجود ندارد
        /// </summary>
        [Description("اجازه دسترسی به بخش مورد نظر وجود ندارد")]
        AccessDenied = 11,
        /// <summary>
        /// اطلاعات مورد نظر قبلا در دیتابیس ثبت شده است
        /// </summary>
        [Description("اطلاعات مورد نظر قبلا در دیتابیس ثبت شده است")]
        DataIsIsAvailable = 12,
        /// <summary>
        /// نام کاربری(شماره تلفن همراه) تکراری است
        /// </summary>
        [Description("نام کاربری(شماره تلفن همراه) تکراری است")]
        UserNameIsAvailable = 13,
        /// <summary>
        /// مقدار کیف پول کافی نیست
        /// </summary>
        [Description("مقدار کیف پول کافی نیست")]
        NotEnoughMoney = 14,
        /// <summary>
        /// رمز به صورت پیامک به شما ارسال شد.
        /// </summary>
        [Description("رمز به صورت پیامک به شما ارسال شد.")]
        SendSmsSuccessful = 15,
        /// <summary>
        /// محصول تکراری وجود دارد
        /// </summary>
        [Description("محصول تکراری وجود دارد")]
        DuplicatePruduct = 16,
        /// <summary>
        /// امتیاز شما کافی نیست
        /// </summary>
        [Description("امتیاز شما کافی نیست")]
        NotEnoughPoints = 17,
        /// <summary>
        /// فاکتوری جهت اضافه کردن موجود نبود
        /// </summary>
        [Description("فاکتوری جهت اضافه کردن موجود نبود")]
        NotFactureIsAvailable = 18,
        /// <summary>
        /// منطقه مورد نظر یافت نشد
        /// </summary>
        [Description("منطقه مورد نظر یافت نشد")]
        RegionNotFound = 19,
        /// <summary>
        /// تعداد دریافت کد شانس بیش از حد مجاز است
        /// </summary>
        [Description("تعداد دریافت کد شانس بیش از حد مجاز است")]
        GetLotteryCodeIsLimited = 20,
        /// <summary>
        /// مقدار کیف پول کافی نیست
        /// </summary>
        [Description("مقدار کیف پول یا امتیاز شما کافی نیست")]
        NotEnoughMoneyOrPoints = 21,
        /// <summary>
        /// این کد کوپن قبلا توسط شما استفاده شده
        /// </summary>
        [Description("این کد کوپن قبلا توسط شما استفاده شده")]
        IsBeforeUsed = 22,
        /// <summary>
        /// ارسال پیامک فعلا غیر فعال است
        /// </summary>
        [Description("ارسال پیامک فعلا غیر فعال است.")]
        SMSServiceIsOff = 23,
        /// <summary>
        /// جمع مقادیر پولی وارد شده کمتر از جمع فاکتور می باشد
        /// </summary>
        [Description("جمع مقادیر پولی وارد شده کمتر از جمع فاکتور می باشد.")]
        TotalFildsNotEnough = 24,
        /// <summary>
        /// جمع مقادیر پولی وارد شده کمتر از جمع فاکتور می باشد
        /// </summary>
        [Description("خطا در ثبت اطلاعات")]
        InsertNotSuccess = 25,

        [Description("لاگین با موفقیت انجام شد")]
        LoginSuccessful = 26,

    }
}