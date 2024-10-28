using DataLayer;
using DomainClass;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Document;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<DocumentEntity> _Entity;

        public DocumentService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<DocumentEntity>();
        }
        async Task<Feedback<IList<DocumentViewModel>>> IDocumentService.GetAllDocument()
        {
            var FbOut = new Feedback<IList<DocumentViewModel>>(); 
            var EntityList =await _Entity.AsNoTracking().Select(x => new DocumentViewModel()
            {
                Id = x.Id,
                Title = x.Title,
            }
            ).ToListAsync();
            if (EntityList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, EntityList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, FbOut.Value, "محتوایی یافت نشد");

        }
    }
}
