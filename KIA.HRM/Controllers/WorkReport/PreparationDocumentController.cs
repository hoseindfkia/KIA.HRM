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

        
        [HttpPost("AddPreparationDocument")]
        public async Task<Feedback<int>> Post(PreparationDocumentPostViewModel PreparationDocumentPost )
        {
            if (!ModelState.IsValid)
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.InvalidDataFormat, Share.Enum.MessageType.Error, 0, ModelState.GetModelStateErrors());
            return await _preparationDocumentService.AddAsycn(PreparationDocumentPost, UserId: 0);
        }

    }
}
