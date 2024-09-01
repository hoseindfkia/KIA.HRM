using System.ComponentModel;

namespace Share.Enum
{
    /// <summary>
    /// تعریف انواع فایلهای قابل آپلود
    /// نام فولدر های داخل آپلود نیز همنام همین نوع ها خواهد بود
    /// </summary>
    public enum FileType
    {
        [Description("تصویر")]
        Image = 0,
        [Description("تصویر بند انگشتی")]
        ThumbnailImage = 1,
        [Description("سند")]
        Document = 2,
        [Description("کلیپ")]
        Clip = 3,
        [Description("صوتی")]
        Sound = 4,
        [Description("نامشخص")]
        Unknown = 5,
        [Description("موقت")]
        Temp = 6,
        [Description("ویدئو یا صدا")]
        VoiceVideo = 7
    }
}
