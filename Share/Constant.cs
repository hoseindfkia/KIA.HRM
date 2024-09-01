using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share
{
    public class Constant
    {
        public const int StringLengthTitle = 256;
        public const int StringLengthDescription = 512;
        public const int StringLengthName = 128;
        public const int StringLengthURL = 1024;
        public const int StringLengthPassword = 128;
        public const int StringLengthEmail = 265;



        /// <summary>
        /// مسیر آپلود فایلها در سرور
        /// </summary>
        public const string UploadPath = "~/Content/Upload";

        #region پسوند های مجاز انواع فایل
        /// <summary>
        /// لیست پسوندهای تصویر
        /// </summary>
        public static readonly string[] ListOfImageExtension = new string[] { "jpg", "jpeg", "gif", "png" };
        /// <summary>
        /// لیست پسوند های سند
        /// </summary>
        public static readonly string[] ListOfDocumentExtension = { "pdf", "ppt", "pptx", "doc", "docx", "txt", "xls", "xlsx", "xpp", "rtf", "accdb", "mdb" };
        /// <summary>
        /// لیست پسوند های صدا
        /// </summary>
        public static readonly string[] ListOfSoundExtension = { "opu", "bmf", "lac", "aif", "act", "aac", "amr", "ape", "au", "awb", "dct", "dss", "dvf", "gsm", "ivs", "m4a", "m4p", "mmf", "mp3", "mpc", "msv", "ogg", "oga", "ra", "rm", "raw", "tta", "vox", "wav", "wma", "wv" };
        /// <summary>
        /// لیست پسوند های کلیپ
        /// </summary>
        public static readonly string[] ListOfClipExtension = { "mpeg", "webm", "mkv", "flv", "vob", "dat", "ogv", "drc", "mng", "avi", "mov", "qt", "wmv", "yuv", "rmvb", "asf", "mp4", "m4p", "m4v", "mpg", "mp2", "mpe", "mpv", "m2v", "svi", "3gp", "3g2", "mxf", "roq" };
        #endregion

        //بعدا می توان این قسمت را توسط پایگاه داده پر نمود
        #region حداکثر اندازه فایلها
        /// <summary>
        /// حداکثر اندازه فایل تصویری
        /// </summary>
        public static int MaximumImageSize = 1000 * 1024;
        /// <summary>
        /// حداکثر اندازه فایل تصویر بند انگشتی
        /// </summary>
        public static int MaximumThumbnailImageSize = 50 * 1024;
        /// <summary>
        /// حداکثر اندازه فایل سند
        /// </summary>
        public static int MaximumDocumnetSize = 2000 * 1024;
        /// <summary>
        /// حداکثر اندازه فایل صوتی
        /// </summary>
        public static int MaximumSoundSize = 5000 * 1024;
        /// <summary>
        /// حداکثر اندازه فایل کلیپ
        /// </summary>
        public static int MaximumClipSize = 10000 * 1024;
        /// <summary>
        /// حداکثر اندازه فایل نامشخص
        /// </summary>
        public static int MaximumUnKnownSize = 10000 * 1024;
        #endregion


        #region کلید انکریپت فایل ها
        public static readonly byte[] Key = new byte[16] { 1, 3, 6, 6, 9, 9, 2, 9, 0, 19, 66, 33, 98, 3, 1, 6 };
        public static readonly byte[] IV = new byte[16] { 1, 3, 6, 6, 9, 9, 2, 9, 0, 19, 66, 33, 98, 3, 1, 6 };

        private byte[] salt { get; set; } = new byte[16] { 200 ,255,4,207,44,11,41,37,45,41,85,87,91,103,49,136 };

        public const string TemporaryFilesPath = "~/Content/Upload/TemporaryFiles";
        #endregion

    }
}
