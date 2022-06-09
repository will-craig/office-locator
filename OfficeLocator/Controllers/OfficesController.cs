using Microsoft.AspNetCore.Mvc;
using OfficeLocator.DAL;
using OfficeLocator.Models;

namespace OfficeLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        
        public OfficesController(IOfficeService officeService)
        {
            _officeService = officeService;
        }
        
        /// <summary>
        /// Gets offices closest to the requested latitude and longitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [HttpGet(Name = "findAll")]
        public IActionResult GetOffices(double latitude, double longitude)
        {
            var offices = _officeService.GetOffices();
            
            return Ok(offices);
        }
        
        /// <summary>
        /// Saves an office
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "saveOffice")]
        public IActionResult SaveOffice(OfficeRequest request)
        {
            return Ok(request);
        }
    }
}
