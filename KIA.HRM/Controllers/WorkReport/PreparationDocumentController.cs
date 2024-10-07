using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.PreparationDocument;
using Share;
using ViewModel.WorkReport.PreparationDocument;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers.WorkReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreparationDocumentController : ControllerBase
    {
        private readonly IPreparationDocumentService _preparationDocumentService;   

        public PreparationDocumentController(IPreparationDocumentService preparationDocumentService)
        {
            _preparationDocumentService = preparationDocumentService;   
        }

        
        [HttpPost]
        public async Task<Feedback<int>> Post(PreparationDocumentPostViewModel PreparationDocumentPost )
        {
            return await _preparationDocumentService.AddAsycn(PreparationDocumentPost, UserId: 0);
        }

    }
}
