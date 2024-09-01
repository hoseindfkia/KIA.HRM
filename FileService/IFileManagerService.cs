using Share;
using Microsoft.AspNetCore.Http;
using Share.Enum;
using DomainClass.Main;

namespace FileService
{
    public interface IFileManagerService
    {
        Feedback<string> Add(FormType FormType, FileType FileTypeForValidation, IFormFile File);
        Feedback<string> Move(FormType FormType, string FilePath, FileType FileTypeForValidation);

        Feedback<string> MoveNew(FormType FormType, string FilePath, FileType FileTypeForValidation, bool IsEncryptFile = false, bool SaveToFTP = false);

        public Feedback<string> EncryptionFile(FormType FormType, string FilePath, Share.Enum.FileType FileTypeForValidation, bool IsDeleteFile = true);
        public Feedback<string> DecryptionFile(FormType FormType, string FilePath, Share.Enum.FileType FileTypeForValidation);


        public Feedback<string> UploadToFtpServer(FormType FormType, string FilePath, FileType FileTypeForValidation, SFTPAccount FTPServerAccount, bool IsEncryptFile = false);
    }
}
