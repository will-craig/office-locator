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
        
        [HttpGet(Name = "findAll")]
        public IActionResult GetOffices()
        {
            var offices = _officeService.GetOffices();
            
            return Ok(offices);
        }
  
        [HttpPost(Name = "search")]
        public IActionResult SearchOffices(OfficeSearchRequest office)
        {
            var allOffices = _officeService.GetOffices();
            
            var offices = allOffices.Where(
                o => o.Latitude == office.Latitude &&
                     o.Longitude == office.Longitude);

            return Ok(offices);
        }
    }
}
