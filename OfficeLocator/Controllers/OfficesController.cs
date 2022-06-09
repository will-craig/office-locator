using Microsoft.AspNetCore.Mvc;
using OfficeLocator.Models;

namespace OfficeLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficesController : ControllerBase
    {
        private static readonly IList<OfficeRequest> Offices = new List<OfficeRequest>()
        {
            new OfficeRequest
            {
                Latitude = 20,
                Longitude = 20,
                Name = "London",
                Wifi = true,
                ExtendedAccess = false,
                MeetingRooms = false,
                Kitchen = false,
                BreakArea = false,
                PetFriendly = false,
                Printing = false,
                Shower = false
            },
            new OfficeRequest
            {
                Latitude = 40,
                Longitude = 30,
                Name = "Birmingham",
                Wifi = true,
                ExtendedAccess = false,
                MeetingRooms = false,
                Kitchen = false,
                BreakArea = false,
                PetFriendly = false,
                Printing = false,
                Shower = false
            }
        };
        
        [HttpGet(Name = "findAll")]
        public IActionResult GetOffices()
        {
            return Ok(Offices);
        }
  
        [HttpPost(Name = "search")]
        public IActionResult SearchOffices(OfficeSearchRequest office)
        {
            var offices = Offices.Where(
                o => o.Latitude == office.Latitude &&
                     o.Longitude == office.Longitude);

            return Ok(offices);
        }
    }
}
