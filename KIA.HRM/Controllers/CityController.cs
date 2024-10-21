using Microsoft.AspNetCore.Mvc;
using Service.City;
using Share;
using ViewModel.City;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        // GET: api/<CityController>
        [HttpGet]
        public async Task<Feedback<IList<CityListViewModel>>> Get(int ProviceId)
        {
            return await _cityService.GetListByProviceIdAsync(ProviceId);
        }

        
    }
}
