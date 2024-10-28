using Microsoft.AspNetCore.Mvc;
using Service.Document;
using Share;
using ViewModel.Document;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        // GET: api/<DocumentController>
        [HttpGet]
        public async Task<Feedback<IList<DocumentViewModel>>> Get()
        {
            return await _documentService.GetAllDocument();
        }

    }
}
