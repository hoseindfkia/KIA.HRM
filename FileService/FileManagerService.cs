using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Share.Enum;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Renci.SshNet;
using DomainClass.Main;
using Renci.SshNet.Common;
using System.Net.Sockets;
using Share.Models;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace FileService
{
    public class FileManagerService : IFileManagerService
    {
        private IHostingEnvironment _environment;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly AppSettings _mySettings;


        /// <summary>
        /// سازنده سرویس فایل
        /// </summary>
        /// <param name="Server">شی سرور جهت تبدیل آدرس مجازی به فیزیکی</param>
        public FileManagerService(IHostingEnvironment environment, IHostingEnvironment hostingEnvironment, IOptions<AppSettings> mySettings)//,  IApplicationEnvironment appEnvironment)
        {
            // _appEnvironment = appEnvironment;
            _environment = environment;
            _hostingEnvironment = hostingEnvironment;
            _mySettings = mySettings.Value;
        }


        /// <summary>
        /// ثبت فایل بر روی سرور
        /// </summary>
        /// <param name="ControllerName">نام کنترلر یا بخشی که فایل در آن آپلود شده است</param>
        /// <param name="FileTypeForValidation">نوع فایل آپلود شده بایستی با این نوع مطابقت داشته باشد</param>
        /// <param name="File">فایل آپلود شده</param>
        /// <returns>آدرس لینک فایل ثبت شده</returns>
        public Feedback<string> Add(FormType FormType, FileType FileTypeForValidation, IFormFile File)
        {
            Feedback<string> Fb = new Feedback<string>();

            FullFileNameMaker CurrentFileValidation = new FullFileNameMaker(FormType, File, FileTypeForValidation);
            Feedback<IList<string>> FbCurrentFileValidation = CurrentFileValidation.MakeFullFileNameWithSizeValidation();
            //در صورتی که فایل معتبر باشد
            if (FbCurrentFileValidation.Status == FeedbackStatus.FetchSuccessful)
            {
                try
                {
                    //نام معتبر فایل
                    string FileName = FbCurrentFileValidation.Value[0];
                    //مسیر لینک فایل معتبر
                    var PathInServer = Path.GetFullPath(_environment.WebRootPath + "\\" + FbCurrentFileValidation.Value[1].Substring(2));


                    //آدرس کامل لینک فایل
                    string FullFileNameLink = FbCurrentFileValidation.Value[2];
                    if (!Directory.Exists(PathInServer))
                    {
                        DirectoryInfo MasterDirectory = Directory.CreateDirectory(PathInServer);
                    }

                    var uploads = Path.Combine(PathInServer);

                    if (File.Length > 0)
                    {
                        using (var FileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                        {
                            File.CopyTo(FileStream);

                        }
                    }


                    Fb.SetFeedback(FeedbackStatus.RegisteredSuccessful, MessageType.Info, FullFileNameLink, null, "");
                }
                //در صورتی که امکان ثبت فایل در سرور نباشد
                catch (Exception Ex)
                {
                    Fb.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, null, Ex.Message);
                }
            }
            else
            {
                Fb.SetFeedback(FbCurrentFileValidation.Status, FbCurrentFileValidation.MessageType, null, "");
            }

            return Fb;
        }

        /// <summary>
        /// جابجا کردن یک فایل به محل دیگر
        /// </summary>
        /// <param name="FilePath">مسیر فایل مبدا</param>
        /// <param name="ControllerName">نام کنتلر برای مقصد</param>
        /// <param name="FileTypeForValidation">نوع فایل برای مقصد</param>
        /// <param name="ApplicationId">کد برنامه مقصد</param>
        /// <returns>مسیر و نام فایل مقصد</returns>
        public Feedback<string> Move(FormType FormType, string FilePath, Share.Enum.FileType FileTypeForValidation)
        {
            Feedback<string> Fb = new Feedback<string>();
            FilePath = Path.GetFullPath(_environment.WebRootPath + "\\" + FilePath.Substring(2));
            FullFileNameMaker RenameFile = new FullFileNameMaker(FormType, FileTypeForValidation);
            Feedback<IList<string>> FbRenameFile = RenameFile.RenameFile(FilePath);
            if (FbRenameFile.Status == FeedbackStatus.FetchSuccessful)
            {
                try
                {
                    //نام معتبر فایل
                    string FileName = FbRenameFile.Value[0];//.ValueList[0];
                                                            //مسیر لینک فایل معتبر
                    string PathInServer = Path.GetFullPath(_environment.WebRootPath + "\\" + FbRenameFile.Value[1].Substring(2));
                    //آدرس کامل لینک فایل
                    string FullFileNameLink = FbRenameFile.Value[2];
                    if (!Directory.Exists(PathInServer))
                    {
                        DirectoryInfo MasterDirectory = Directory.CreateDirectory(PathInServer);
                    }
                    System.IO.File.Move(FilePath, PathInServer + "\\" + FileName);
                    Fb.SetFeedback(FeedbackStatus.UpdatedSuccessful, MessageType.Info, FullFileNameLink, null, "");
                }
                catch (Exception Ex)
                {
                    Fb.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, "", null, Ex.Message);
                }
            }
            else
            {
                Fb.SetFeedback(FbRenameFile.Status, FbRenameFile.MessageType, "", null, "");
            }
            return Fb;
        }

        /// <summary>
        ///    متد جدید انتقال فایل -انتقال فایل به مکان دیگر
        /// فایل ها در چهر حالت مختلف ذخیره می شود: درلوکال یا سرور اف تی پی یا ذخیره به صورت انکریپت یا غیر انکریپت
        /// </summary>
        /// <param name="FormType"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileTypeForValidation"></param>
        /// <param name="IsEncryptFile">فایل انکریپت شود؟</param>
        /// <param name="SaveToFTP">فایل در سرور اف تی پی ذخیره شود یا خیر؟</param>
        /// <returns></returns>
        public Feedback<string> MoveNew(FormType FormType, string FilePath, FileType FileTypeForValidation, bool IsEncryptFile = false, bool SaveToFTP = false)
        {
            var FbOut = new Feedback<string>();
            var GenerateFile = GenerateNameAndPathFile(FormType, FilePath, FileTypeForValidation);
            var FileProperty = GenerateFile.Value;
            // تغییر نام فایل با موفقیت انجام شد یا خیر
            if (GenerateFile.Status == FeedbackStatus.FetchSuccessful)
            {
                // Ftp ذخیره در 
                if (SaveToFTP)
                {
                    // انکریپت فایل انجام شود یا خیر
                    if (IsEncryptFile)
                    {
                        //TODO: دو آپشن برای کلید اینجا تعریف شود یکی کلید خصوصی و یکی کلید عمومی
                        // فایل انکریپت شد و در فولدر تمپراری ذخیره شد
                        var EncryptedFile = EncryptionFileNew(FileProperty, Constant.Key, Constant.IV);
                        if (EncryptedFile.Status == FeedbackStatus.RegisteredSuccessful)
                        {
                            var OriginalFile = FileProperty.FileOldPath;
                            // می بایستی فایل درون تمپلیت برای آپلود فرستاده شود
                            FileProperty.FileOldPath = EncryptedFile.Value;
                            // ذخیره شود ftp اکنون می بایستی در  
                            var newPathInFTP = UploadToFtpServerNew(FileProperty);
                            // فایل با موفقیت در سرور ذخیره شد
                            if (newPathInFTP.Status == FeedbackStatus.RegisteredSuccessful)
                            {
                                RemoveFile(FileProperty.FileOldPath);
                                RemoveFile(OriginalFile);
                                FbOut.SetFeedback(FeedbackStatus.UpdatedSuccessful, MessageType.Info, FileProperty.FullFileNameLink, null, "");
                            }
                        }
                    }
                    else
                    {
                        // ذخیره شود ftp اکنون می بایستی در  
                        var newPathInFTP = UploadToFtpServerNew(FileProperty);
                        if (newPathInFTP.Status == FeedbackStatus.RegisteredSuccessful)
                        {
                            var removRes = RemoveFile(FileProperty.FileOldPath);
                            FbOut.SetFeedback(FeedbackStatus.UpdatedSuccessful, MessageType.Info, FileProperty.FullFileNameLink, null, "");
                        }
                    }
                }
                // ذخیره در لوکال
                else
                {
                    // انکریپت فایل انجام شود یا خیر
                    if (IsEncryptFile)
                    {
                        //TODO: دو آپشن برای کلید اینجا تعریف شود یکی کلید خصوصی و یکی کلید عمومی
                        // فایل انکریپت شد و در فولدر تمپراری ذخیره شد
                        var EncryptedFile = EncryptionFileNew(FileProperty, Constant.Key, Constant.IV);
                        if (EncryptedFile.Status == FeedbackStatus.RegisteredSuccessful)
                        {
                            if (!Directory.Exists(FileProperty.PathInServer))
                            {
                                DirectoryInfo MasterDirectory = Directory.CreateDirectory(FileProperty.PathInServer);
                            }
                            System.IO.File.Move(EncryptedFile.Value, FileProperty.PathInServerWithFileName);
                            RemoveFile(FileProperty.FileOldPath);
                            FbOut.SetFeedback(FeedbackStatus.UpdatedSuccessful, MessageType.Info, FileProperty.FullFileNameLink, null, "");
                        }
                    }
                    // حالت غیر انکریپت ذخیره شود
                    else
                    {

                        try
                        {
                            if (!Directory.Exists(FileProperty.PathInServer))
                            {
                                DirectoryInfo MasterDirectory = Directory.CreateDirectory(FileProperty.PathInServer);
                            }

                            System.IO.File.Move(FileProperty.FileOldPath, FileProperty.PathInServerWithFileName);
                            FbOut.SetFeedback(FeedbackStatus.UpdatedSuccessful, MessageType.Info, FileProperty.FullFileNameLink, null, "");
                        }
                        catch (Exception Ex)
                        {
                            FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, "", null, Ex.Message);
                        }
                    }
                }
            }
            else
                FbOut.SetFeedback(GenerateFile.Status, GenerateFile.MessageType, "", null, "");

            return FbOut;
        }

        /// <summary>
        /// یک فایل به صورت انکریپت ایجاد می کند سپس در فولدر تمپراری فایل ذخیره می شود. 
        /// پس از پایان کار فایل تمپراری شده باید از فولدر مربوطه حذف گردد
        /// </summary>
        /// <param name="FormType"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileTypeForValidation"></param>
        /// <returns></returns>
        public Feedback<string> EncryptionFileNew(FileProperty fileProperty, byte[] Key, byte[] IV)
        {
            var FbOut = new Feedback<string>();
            try
            {
                string outputFilePath = _environment.WebRootPath + _mySettings.TemporaryFilePath + "\\" + fileProperty.FileNewName;
                using (FileStream fsInput = new FileStream(fileProperty.FileOldPath, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(outputFilePath, FileMode.Create))
                    {
                        using (AesManaged aes = new AesManaged())
                        {
                            aes.Key = Key;
                            aes.IV = IV;
                            // Perform encryption
                            ICryptoTransform encryptor = aes.CreateEncryptor();
                            using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }
                FbOut.SetFeedback(FeedbackStatus.RegisteredSuccessful, MessageType.Info, outputFilePath, "");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, null, ex.Message);
            }
            return FbOut;

        }

        /// <summary>
        /// قدیمی- پس از اطمینان از عملکرد جدید این یکی حذف گردد
        /// </summary>
        /// <param name="FormType"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileTypeForValidation"></param>
        /// <param name="IsDeleteFile"></param>
        /// <returns></returns>
        public Feedback<string> EncryptionFile(FormType FormType, string FilePath, Share.Enum.FileType FileTypeForValidation, bool IsDeleteFile = true)
        {
            var FbOut = new Feedback<string>();
            FilePath = Path.GetFullPath(_environment.WebRootPath + "\\" + FilePath.Substring(2));
            FullFileNameMaker RenameFileName = new FullFileNameMaker(FormType, FileTypeForValidation);
            Feedback<IList<string>> FbRenameFile = RenameFileName.RenameFile(FilePath);
            //مسیر لینک فایل معتبر
            string PathInServer = Path.GetFullPath(_environment.WebRootPath + "\\" + FbRenameFile.Value[1].Substring(2));
            try
            {
                string outputFilePath = PathInServer + "\\" + FbRenameFile.Value[0];
                using (FileStream fsInput = new FileStream(FilePath, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(outputFilePath, FileMode.Create))
                    {
                        using (AesManaged aes = new AesManaged())
                        {
                            aes.Key = Constant.Key;
                            aes.IV = Constant.IV;
                            //aes.GenerateKey();
                            //aes.GenerateIV();

                            // Perform encryption
                            ICryptoTransform encryptor = aes.CreateEncryptor();
                            using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }

                            if (IsDeleteFile)
                                System.IO.File.Delete(FilePath);
                        }
                    }
                }
                FbOut.SetFeedback(FeedbackStatus.RegisteredSuccessful, MessageType.Info, outputFilePath, "");

            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, null, ex.Message);
            }
            return FbOut;
        }

        /// <summary>
        /// دیکریپت کردن فایل - باید متد جدید بنویسم
        /// </summary>
        /// <param name="FormType"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileTypeForValidation"></param>
        /// <returns></returns>
        public Feedback<string> DecryptionFile(FormType FormType, string FilePath, FileType FileTypeForValidation)
        {

            var FbOut = new Feedback<string>();
            FilePath = Path.GetFullPath(_environment.WebRootPath + "\\" + FilePath.Substring(2));
            FullFileNameMaker RenameFile = new FullFileNameMaker(FormType, FileTypeForValidation);
            Feedback<IList<string>> FbRenameFile = RenameFile.RenameFile(FilePath);

            //مسیر لینک فایل معتبر
            string PathInServer = Path.GetFullPath(_environment.WebRootPath + "\\" + FbRenameFile.Value[1].Substring(2));

            try
            {
                string outputFilePath = Path.GetFullPath(_environment.WebRootPath + "\\" + Constant.TemporaryFilesPath.Substring(2) + "\\" + FbRenameFile.Value[0]);

                // Decryption
                using (FileStream fsInput = new FileStream(FilePath, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(outputFilePath, FileMode.Create))
                    {
                        using (AesManaged aes = new AesManaged())
                        {
                            // Retrieve the key and IV used for encryption
                            aes.Key = Constant.Key;
                            aes.IV = Constant.IV;

                            // Perform decryption
                            ICryptoTransform decryptor = aes.CreateDecryptor();
                            using (CryptoStream cs = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }


                FbOut.SetFeedback(FeedbackStatus.RegisteredSuccessful, MessageType.Info, outputFilePath, "");

            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, null, ex.Message);
            }
            return FbOut;

        }

        /// <summary>
        /// قدیمی- پس از اطمینان از عملکرد جدید این یکی حذف گردد
        /// </summary>
        /// <param name="FormType"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileTypeForValidation"></param>
        /// <param name="FTPServerAccount"></param>
        /// <param name="IsEncryptFile"></param>
        /// <returns></returns>
        public Feedback<string> UploadToFtpServer(FormType FormType, string FilePath, FileType FileTypeForValidation, SFTPAccount FTPServerAccount, bool IsEncryptFile = false)
        {
            var FbOut = new Feedback<string>();
            FilePath = Path.GetFullPath(_environment.WebRootPath + "\\" + FilePath.Substring(2));
            Feedback<string> Fb = new Feedback<string>();
            FullFileNameMaker RenameFile = new FullFileNameMaker(FormType, FileTypeForValidation);
            Feedback<IList<string>> FbRenameFile = RenameFile.RenameFile(FilePath);
            if (FbRenameFile.Status == FeedbackStatus.FetchSuccessful)
            {

                using SftpClient client = new(FTPServerAccount.Host, FTPServerAccount.Port, FTPServerAccount.Username, FTPServerAccount.Password);
                try
                {
                    //نام معتبر فایل
                    string FileName = FbRenameFile.Value[0];//.ValueList[0];
                                                            //مسیر لینک فایل معتبر
                    string PathInServer = FbRenameFile.Value[3] + FileName;  ///Path.GetFullPath("\\" + FbRenameFile.Value[1].Substring(2));
                    client.Connect();
                    if (client.IsConnected)
                    {
                        client.CreateDirectory(FbRenameFile.Value[3]);
                        client.UploadFile(System.IO.File.OpenRead(FilePath), PathInServer);

                        client.Disconnect();
                        FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, PathInServer, "");
                    }
                    FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", "Couldn't Connect to FTP Server");
                }
                catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
                {
                    FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Error connecting to server: {e.Message}");
                }
                catch (SshAuthenticationException e)
                {
                    FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Failed to authenticate: {e.Message}");
                }
                catch (SftpPermissionDeniedException e)
                {
                    FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Operation denied by the server: {e.Message}");
                }
                catch (SshException e)
                {
                    FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Sftp Error: {e.Message}");
                }
            }
            else
            {
                Fb.SetFeedback(FbRenameFile.Status, FbRenameFile.MessageType, "", "Rename File error");
            }
            return FbOut;
        }

        /// <summary>
        /// آپلود فایل به سرور اف تی پی
        /// </summary>
        /// <param name="fileProperty"></param>
        /// <returns></returns>
        public Feedback<string> UploadToFtpServerNew(FileProperty fileProperty)
        {
            SFTPAccount FTPServerAccount = _mySettings.FTPServerAccount;
            var FbOut = new Feedback<string>();
            using SftpClient client = new(FTPServerAccount.Host, FTPServerAccount.Port, FTPServerAccount.Username, FTPServerAccount.Password);
            try
            {
                // 
                string PathInServer = fileProperty.FullPathFTP + "/" + fileProperty.FileNewName;
                client.Connect();
                if (client.IsConnected)
                {

                    client.CreateDirectory(fileProperty.FullPathFTP);

                    using (FileStream fileStream = new FileStream(fileProperty.FileOldPath, FileMode.Open, FileAccess.Read))
                    {
                        client.UploadFile(fileStream, PathInServer);
                    }


                    //   client.UploadFile(File.OpenRead(fileProperty.FileOldPath), PathInServer);

                    client.Disconnect();

                    FbOut.SetFeedback(FeedbackStatus.RegisteredSuccessful, MessageType.Info, PathInServer, "");
                }
                else
                    FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", "Couldn't Connect to FTP Server");
            }
            catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Error connecting to server: {e.Message}");
            }
            catch (SshAuthenticationException e)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Failed to authenticate: {e.Message}");
            }
            catch (SftpPermissionDeniedException e)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Operation denied by the server: {e.Message}");
            }
            catch (SshException e)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Warninig, "", $"Sftp Error: {e.Message}");
            }

            return FbOut;
        }

        /// <summary>
        /// مسیر فایل را دریافت سپس نام جدی برای فایل انتخاب می کند سپس مسیر ها را به فرمت فیزیکی آماده می کند تا برای دانلود آماده اشد
        /// </summary>
        /// <param name="FormType"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileTypeForValidation"></param>
        /// <returns></returns>
        public Feedback<FileProperty> GenerateNameAndPathFile(FormType FormType, string FilePath, Share.Enum.FileType FileTypeForValidation)
        {
            var FbOut = new Feedback<FileProperty>();
            // ایجاد مسیر فیزیکی فایل جهت دسترسی به فایل
            var OldName = FilePath.Substring(2);
            FilePath = Path.GetFullPath(_environment.WebRootPath + "\\" + FilePath.Substring(2));
            FullFileNameMaker RenameFile = new FullFileNameMaker(FormType, FileTypeForValidation);
            // تغییر نام فایل
            var FbRenameFile = RenameFile.RenameFileNew(FilePath);

            if (FbRenameFile.Status == FeedbackStatus.FetchSuccessful)
            {
                FbRenameFile.Value.FileOldPath = _environment.WebRootPath + "\\" + OldName.Replace("/", "\\");
                FbRenameFile.Value.PathInServer = Path.GetFullPath(_environment.WebRootPath + "\\" + FbRenameFile.Value.Path.Substring(2));
                FbRenameFile.Value.PathInServerWithFileName = Path.GetFullPath(_environment.WebRootPath + "\\" + FbRenameFile.Value.FullFileNewName.Substring(2));
                FbOut.SetFeedback(FbRenameFile.Status, FbRenameFile.MessageType, FbRenameFile.Value, "");
            }
            else
                FbOut.SetFeedback(FbRenameFile.Status, FbRenameFile.MessageType, null, "");

            return FbOut;
        }

        /// <summary>
        /// حذف فایل
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public Feedback<string> RemoveFile(string Path)
        {
            var FbOut = new Feedback<string>();
            try
            {
                System.IO.File.Delete(Path);
                FbOut.SetFeedback(FeedbackStatus.DeletedSuccessful, MessageType.Info, "", "");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, "", "Delete File Error!");
            }
            return FbOut;
        }


    }
}
