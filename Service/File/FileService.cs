using DataLayer;
using DomainClass;
using DomainClass.Main;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;

namespace Service.File
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<FileEntity> _Entity;
        private readonly AppSettings _mySettings;
        public FileService(IUnitOfWorkContext context, IOptions<AppSettings> mySettings)
        {
            _Context = context;
            _Entity = _Context.Set<FileEntity>();
            _mySettings = mySettings.Value;
        }

        public async Task<Feedback<IList<FileEntity>>> AddRangeAsycn(IList<FilePostViewModel> FilePost, long UserId, bool SaveChange=false)
        {
            var FbOut = new Feedback<IList<FileEntity>>();
            if (FilePost != null)
            {
                var IsEncryptFiles = _mySettings.IsEncryptionFiles;
                var FileModelList = FilePost.Select(x => new FileEntity()
                {
                    CreatedDate = DateTime.Now,
                    FileName = x.FileName,
                    FormType = x.FormType,
                    FileType = x.FileType,
                    IsEncrypt = IsEncryptFiles,
                    Url = x.Url,
                }).ToList();
                _Entity.AddRange(FileModelList);
                if (SaveChange)
                    await _Context.SaveChangesAsync();
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.UpdatedSuccessful, Share.Enum.MessageType.Info, FileModelList, "");
            }
            else
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, FbOut.Value, "");
        }
    }
}
