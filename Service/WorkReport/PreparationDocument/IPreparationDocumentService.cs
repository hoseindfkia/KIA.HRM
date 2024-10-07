using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WorkReport.Mission;
using ViewModel.WorkReport.PreparationDocument;

namespace Service.WorkReport.PreparationDocument
{
    public interface IPreparationDocumentService
    {
        Task<Feedback<int>> AddAsycn(PreparationDocumentPostViewModel PreparationDocumentPost, long UserId);

        Task<Feedback<IList<PreparationDocumentViewModel>>> GetByDateAsync(DateTime dateTime, long UserId);

    }
}
