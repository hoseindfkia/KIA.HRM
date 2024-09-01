using Microsoft.AspNetCore.Mvc;
using Share.Enum;
using Share;
using FileService;
using System.Collections.Generic;
using DomainClass.Main;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    /// <summary>
    /// فایل
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        readonly IFileManagerService _FileManagerService;
        private readonly AppSettings _mySettings;
        public FileManagerController(IFileManagerService fileManagerService, IOptions<AppSettings> mySettings)
        {
            _FileManagerService = fileManagerService;
            _mySettings = mySettings.Value;
        }

        /// <summary>
        /// ثبت یک فایل
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("UploadSingleFile")]
        public Feedback<string> UploadSingleFile(ICollection<IFormFile> files, FormType formType)
        {
            Feedback<string> ResultListOut = new Feedback<string>();
            var fileOnRequest = Request.Form.Files[0];
            if (fileOnRequest != null)
            {
                ResultListOut = _FileManagerService.Add(formType, FileType.Temp, fileOnRequest);
            }

            //if (files.Any())
            //{
            //    foreach (var file in files)
            //    {
            //        if (file.Length > 0)
            //        {
            //            ResultListOut = _FileManagerService.Add(formType, FileType.Temp, file);
            //        }
            //    }
            //}
            //else
            //{
            //    ResultListOut = _FileManagerService.Add(formType, FileType.Temp, fileOnRequest);
            //}
            return ResultListOut;
        }


        [HttpGet("EncryptionFile")]
        public Feedback<string> EncryptionFile(FormType FormType, string FilePath, FileType FileTypeForValidation)
        {
            return _FileManagerService.EncryptionFile(FormType, FilePath, FileTypeForValidation);
        }

        [HttpGet("DecryptionFile")]
        public Feedback<string> DecryptionFile(FormType FormType, string FilePath, FileType FileTypeForValidation)
        {
            return _FileManagerService.DecryptionFile(FormType, FilePath, FileTypeForValidation);
        }

        [HttpGet("UploadToFtpServer")]
        public Feedback<string> UploadToFtpServer(FormType FormType, string FilePath, FileType FileTypeForValidation)
        {
            var ftp = _mySettings.FTPServerAccount;
            return _FileManagerService.UploadToFtpServer(FormType, FilePath, FileTypeForValidation, ftp);
        }


        [HttpGet("MoveNew")]
        public Feedback<string> MoveNew(FormType FormType, string FilePath, FileType FileTypeForValidation, bool IsEncryptFile = false, bool SaveToFTP = false)
        {
            return _FileManagerService.MoveNew(FormType, FilePath, FileTypeForValidation,IsEncryptFile,SaveToFTP);
        }


         [HttpGet("Move")]
        public Feedback<string> Move(FormType FormType, string FilePath, FileType FileTypeForValidation)
        {
            return _FileManagerService.Move(FormType, FilePath, FileTypeForValidation);
        }





    }
}
