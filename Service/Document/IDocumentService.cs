using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Document;

namespace Service.Document
{
    public interface IDocumentService
    {
        Task<Feedback<IList<DocumentViewModel>>> GetAllDocument();
    }
}
