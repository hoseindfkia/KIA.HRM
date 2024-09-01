using Share.Enum;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Share.Models;

namespace Share
{
    /// <summary>
    /// کلاس جهت بررسی فایلی که قرار است آپلود شود
    /// وتولید نام و مسیر فایل جهت ذخیره سازی
    /// </summary>
    public class FullFileNameMaker
    {
        private IFormFile CurrentFile;
        private FileType FileTypeForValidation;
        private FormType CurrentFormType;
        /// <summary>
        /// سازنده کلاس جهت معتبر بودن فایل
        /// </summary>
        /// <param name="File">فایل آپلود شده از طرف کاربر</param>
        /// <param name="FileTypeForValidation">نوع فایلی که بایستی کاربر فرستاده باشد</param>
        /// <param name="ServiceName">نام کنترلر یا بخشی که این فایل آپلود شده است</param>
        /// <param name="ApplicationId">کد برنامه ای که بایستی آپلود شود</param>
        public FullFileNameMaker(FormType FormType, IFormFile File, FileType FileTypeForValidation)
        {
            CurrentFile = File;
            CurrentFormType = FormType;
            this.FileTypeForValidation = FileTypeForValidation;
        }
        public FullFileNameMaker(FormType FormType, FileType FileTypeForValidation)
        {
            CurrentFormType = FormType;
            this.FileTypeForValidation = FileTypeForValidation;
        }

        /// <summary>
        /// در این متد نوع فایل از طریق پسوند آن مشخص می گردد
        /// </summary>        
        /// <param name="FileExtension">پسوند فایل</param>
        /// <returns></returns>
        private FileType GetFileType(string FileExtension)
        {
            FileType FileType = FileType.Unknown;
            bool TypeOfFolderIsFound = false;
            //در صورتی که پسوند تصویری باشد
            foreach (var Item in Constant.ListOfImageExtension)
            {
                if (Item.ToLower() == FileExtension.ToLower())
                {
                    TypeOfFolderIsFound = true;
                    //در صورتی که تصویر بند انگشتی نباشد
                    if (FileTypeForValidation != Enum.FileType.ThumbnailImage)
                        FileType = FileType.Image;
                    //در صورتی که تصویر بند انگشتی باشد
                    else
                        FileType = FileType.ThumbnailImage;
                    break;
                }
            }
            //در صورتی که پسوند سند باشد
            if (TypeOfFolderIsFound == false)
            {
                foreach (var Item in Constant.ListOfDocumentExtension)
                {
                    if (Item.ToLower() == FileExtension.ToLower())
                    {
                        TypeOfFolderIsFound = true;
                        FileType = FileType.Document;
                        break;
                    }
                }
            }
            //در صورتی که پسوند صوتی باشد
            if (TypeOfFolderIsFound == false)
            {
                foreach (var Item in Constant.ListOfSoundExtension)
                {
                    if (Item.ToLower() == FileExtension.ToLower())
                    {
                        TypeOfFolderIsFound = true;
                        FileType = FileType.VoiceVideo;
                        break;
                    }
                }
            }
            //در صورتی که پسوند کلیپ باشد
            if (TypeOfFolderIsFound == false)
            {
                foreach (var Item in Constant.ListOfClipExtension)
                {
                    if (Item.ToLower() == FileExtension.ToLower())
                    {
                        TypeOfFolderIsFound = true;
                        FileType = FileType.VoiceVideo;
                        break;
                    }
                }
            }
            return FileType;
        }
        /// <summary>
        /// دریافت اندازه حداکثر فایل جهت آپلود 
        /// </summary>
        /// <param name="CurrentFileType">نوع فایل</param>
        /// <returns></returns>
        private int GetMaximumFileSize(FileType CurrentFileType)
        {
            int MaximumFileSize = 0;
            switch (CurrentFileType)
            {
                case FileType.Image:
                    MaximumFileSize = Constant.MaximumImageSize;
                    break;
                case FileType.ThumbnailImage:
                    MaximumFileSize = Constant.MaximumImageSize;
                    break;
                case FileType.Document:
                    MaximumFileSize = Constant.MaximumDocumnetSize;
                    break;
                case FileType.Clip:
                    MaximumFileSize = Constant.MaximumClipSize;
                    break;
                case FileType.Sound:
                    MaximumFileSize = Constant.MaximumSoundSize;
                    break;
                default:
                    MaximumFileSize = Constant.MaximumUnKnownSize;
                    break;
            }
            return MaximumFileSize;
        }
        /// <summary>
        /// دریافت نام فولدر جهت ذخیره سازی فایل
        /// در حالت فایل از نوع موقت در یک پوشه موقت که مابین تمامی برنامه ها مشترک است فایل مورد نظر ذخیره می گردد
        /// </summary>
        /// <param name="CurrentFileType">نوع فایل</param>
        /// <returns></returns>
        private string GetFolderName(FileType CurrentFileType)
        {
            string FolderName = "";
            if (CurrentFileType != FileType.Temp)
                FolderName = Constant.UploadPath + "/" + Utility.GetNameOfEnum(typeof(FileType), CurrentFileType) + "/" + Utility.GetNameOfEnum(typeof(FormType), CurrentFormType);
            else
                FolderName = Constant.UploadPath + "/" + Utility.GetNameOfEnum(typeof(FileType), CurrentFileType);
            return FolderName;
        }
        /// <summary>
        /// دریافت نام معتبر فایل جهت ذخیره سازی 
        /// </summary>                
        /// <returns>نام معتبر فایل همراه بدون پسوند</returns>
        private string GetFileName()
        {
            string FileName = "";
            Guid CurrentGuid = Guid.NewGuid();
            FileName += CurrentGuid.ToString();
            string[] IllegalCharacters = new string[] { "'", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "{", "}", "[", "]", ":", ";", @"\", "|", "/", "?", "\"", "/", "~", "`", ",", "." };
            foreach (string Item in IllegalCharacters)
            {
                FileName = FileName.Replace(Item, "");
            }
            return FileName.ToLower();
        }
        /// <summary>
        /// معتبر بودن فایل جهت ذخیره سازی
        /// </summary>
        /// <returns>تولید نام فایل جهت ذخیره بر روی سرور
        /// خروجی به صورت 0نام فایل ، 1مسیر فایل، 2نام ومسیر فایل</returns>
        public Feedback<IList<string>> MakeFullFileNameWithSizeValidation()
        {
            Feedback<IList<string>> Fb = new Feedback<IList<string>>();
            string PathOfFile = "";
            string FileName = "";
            string FullFileName = "";
            List<string> ListOfFileInformation = null;
           
            // درصورت موجود بودن فایل
            if (CurrentFile != null)
            {
                //اسم فایلی که با پسوند است را به 2 فیلد از آرایه تبدیل میکند . فیلد اول نام فایل و فیلد دوم پسوند فایل               
                string[] FileNameSeperated = ContentDispositionHeaderValue.Parse(CurrentFile.ContentDisposition).FileName.Split('.');


                //اگر فایل موجود باشد یا فایلد دارای پسوند باشد
                if (FileNameSeperated != null && FileNameSeperated.Length > 0)
                {
                    FileNameSeperated[0]=  FileNameSeperated[0].Trim('"');
                    FileType CurrentFileType;
                    int MaximumFileSize;
                    string ExtensionOfFile = "";
                    //فایل پسوند ندارد نوع فایل از طریق این تابع مشخص می شود.
                    if (FileNameSeperated.Length == 1)
                    {                       
                        CurrentFileType = GetFileType(ExtensionOfFile);
                    }                        
                    //فایل پسوند دارد
                    else
                    {
                        FileNameSeperated[1] = FileNameSeperated[1].Trim('"');
                        //پسوند فایل
                        ExtensionOfFile = FileNameSeperated[FileNameSeperated.Length - 1];
                        //نوع فایل
                        CurrentFileType = GetFileType(ExtensionOfFile);
                    }
                    //بررسی معتبر بودن نوع فایل
                    if (CurrentFileType == FileTypeForValidation || FileTypeForValidation == FileType.Unknown || FileTypeForValidation == FileType.Temp)
                    {
                        MaximumFileSize = GetMaximumFileSize(CurrentFileType);
                        if (CurrentFile.Length <= MaximumFileSize && CurrentFile.Length > 0)
                        {
                            //دریافت نام معتبر
                            FileName = GetFileName() + "." + ExtensionOfFile;
                            //دریافت مسیر معتبر                            
                            if (FileTypeForValidation == FileType.Unknown)
                                PathOfFile = GetFolderName(CurrentFileType);
                            else
                                PathOfFile = GetFolderName(FileTypeForValidation);
                            //نام و مسیر معتبر
                            FullFileName = PathOfFile + "/" + FileName;
                            ListOfFileInformation = new List<string>();
                            ListOfFileInformation.Add(FileName);
                            ListOfFileInformation.Add(PathOfFile);
                            ListOfFileInformation.Add(FullFileName);
                            Fb.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, ListOfFileInformation, "");
                        }
                        else
                            Fb.SetFeedback(FeedbackStatus.FileIsNotValid, MessageType.Warninig, null, "");
                    }
                    //نوع فایل با نوع مورد نیاز هماهنگ نمی باشد
                    else
                    {
                        Fb.SetFeedback(FeedbackStatus.FileIsNotValid, MessageType.Warninig,  null, "");
                    }
                }
                //نام فایل معتبر نیست
                else
                {
                    Fb.SetFeedback(FeedbackStatus.FileIsNotValid, MessageType.Warninig,  null, "");
                }
            }
            //فایلی آپلود نشده است
            else
            {
                Fb.SetFeedback(FeedbackStatus.FileIsNotFound, MessageType.Warninig, null, "");
            }
            return Fb;
        }
        /// <summary>
        /// ایجاد نام جدید با استفاده از نام فایل قبلی و نوع فایل
        /// </summary>
        /// <param name="PrevFilePath"></param>
        /// <returns></returns>
        public Feedback<IList<string>> RenameFile(string PrevFilePath)
        {
            Feedback<IList<string>> Fb = new Feedback<IList<string>>();
            string PathOfFile = "";
            string FileName = "";
            string FullFileName = "";
            List<string> ListOfFileInformation = new List<string>();
            string[] FileNameSeperated = PrevFilePath.Trim('"').Split('.');
            if (FileNameSeperated != null && FileNameSeperated.Length > 0)
            {
                FileType CurrentFileType;
                string ExtensionOfFile = "";
                //فایل پسوند ندارد
                if (FileNameSeperated.Length == 1)
                    CurrentFileType = GetFileType(ExtensionOfFile);
                //فایل پسوند دارد
                else
                {
                    ExtensionOfFile = FileNameSeperated[FileNameSeperated.Length - 1];
                    CurrentFileType = GetFileType(ExtensionOfFile);
                }
                //بررسی معتبر بودن نوع فایل
                if (CurrentFileType == FileTypeForValidation || FileTypeForValidation == FileType.Unknown || FileTypeForValidation == FileType.Temp)
                {
                    //دریافت نام معتبر
                    FileName = GetFileName() + "." + ExtensionOfFile;
                    //دریافت مسیر معتبر
                    PathOfFile = GetFolderName(CurrentFileType);
                    //نام و مسیر معتبر
                    FullFileName = PathOfFile + "/" + FileName;
                    ListOfFileInformation.Add(FileName);
                    ListOfFileInformation.Add(PathOfFile);
                    ListOfFileInformation.Add(FullFileName);
                    ListOfFileInformation.Add(PathOfFile.Substring(2));
                    Fb.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info,  ListOfFileInformation, "");
                }
                //نوع فایل با نوع مورد نیاز هماهنگ نمی باشد
                else
                {
                    Fb.SetFeedback(FeedbackStatus.FileIsNotValid, MessageType.Warninig, null, "");
                }
            }
            //نام فایل معتبر نیست
            else
            {
                Fb.SetFeedback(FeedbackStatus.FileIsNotValid, MessageType.Warninig, null, "");
            }
            return Fb;
        }

        /// <summary>
        /// ایجاد نام جدید با استفاده از نام فایل قبلی و نوع فایل
        /// </summary>
        /// <param name="PrevFilePath"></param>
        /// <returns></returns>
        public Feedback<FileProperty> RenameFileNew(string PrevFilePath)
        {
            var FbOut = new Feedback<FileProperty>();
            FileProperty FileInformation = new FileProperty();
            string[] FileNameSeperated = PrevFilePath.Trim('"').Split('.');
            if (FileNameSeperated != null && FileNameSeperated.Length > 0)
            {
                FileType CurrentFileType;
                string ExtensionOfFile = "";
                //فایل پسوند ندارد
                if (FileNameSeperated.Length == 1)
                    CurrentFileType = GetFileType(ExtensionOfFile);
                //فایل پسوند دارد
                else
                {
                    ExtensionOfFile = FileNameSeperated[FileNameSeperated.Length - 1];
                    CurrentFileType = GetFileType(ExtensionOfFile);
                }
                //بررسی معتبر بودن نوع فایل
                if (CurrentFileType == FileTypeForValidation || FileTypeForValidation == FileType.Unknown || FileTypeForValidation == FileType.Temp)
                {
                    //دریافت نام معتبر
                    FileInformation.FileNewName = GetFileName() + "." + ExtensionOfFile;
                    //دریافت مسیر معتبر
                    FileInformation.Path = GetFolderName(CurrentFileType);
                    //نام و مسیر معتبر
                    FileInformation.FullFileNewName = FileInformation.Path + "/" + FileInformation.FileNewName;
                    // ftp مسیر فایل در 
                    FileInformation.FullPathFTP = FileInformation.Path.Substring(2);
                    FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, FileInformation, "");
                }
                //نوع فایل با نوع مورد نیاز هماهنگ نمی باشد
                else
                {
                    FbOut.SetFeedback(FeedbackStatus.FileIsNotValid, MessageType.Warninig, FileInformation, "");
                }
            }
            //نام فایل معتبر نیست
            else
            {
                FbOut.SetFeedback(FeedbackStatus.FileIsNotValid, MessageType.Warninig, FileInformation, "");
            }
            return FbOut;
        }


    }
}
