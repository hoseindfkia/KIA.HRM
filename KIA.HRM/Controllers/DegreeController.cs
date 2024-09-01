using Microsoft.AspNetCore.Mvc;
using Service.Degree;
using Share;
using ViewModel.Degree;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreeController : ControllerBase
    {
        private readonly IDegreeService _degreeServices;
        public DegreeController(IDegreeService degreeService)
        {
            _degreeServices = degreeService;
        }

        /// <summary>
        /// دریافت لیست مدارک جهت دراپ داون
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllDropDown")]
        public Task<Feedback<IList<DegreeGetAllDropDownViewModel>>> GetAllDropDownAsync()
        {
            return _degreeServices.GetAllDropDownAsync();
        }


        [HttpGet("test")]
        public Task<Feedback<IList<DegreeGetAllDropDownViewModel>>> Get()
        {
            return _degreeServices.Test();
        }

    }
}
