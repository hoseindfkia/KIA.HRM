using Microsoft.AspNetCore.Mvc;
using Service.Province;
using Share;
using ViewModel.Province;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _provinceService;
        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService; 
        }

        // GET: api/<ProvinceController>
        [HttpGet]
        public async Task<Feedback<IList<ProvinceListViewModel>>> Get()
        {
            return await _provinceService.GettAllProvince();
        }

       
    }
}
