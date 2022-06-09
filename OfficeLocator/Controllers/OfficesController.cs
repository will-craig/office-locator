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
                Id = 1,
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
                Id = 2,
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
        
        [HttpGet("{id}", Name = "findById")]
        public IActionResult GetOfficeById(int id)
        {
            var office = Offices.SingleOrDefault(o => o.Id == id);
            
            return Ok(office);
        }

        [HttpPost(Name = "saveOffice")]
        public IActionResult SaveOffice(OfficeRequest office)
        {
            return Ok("Office saved");
        }
    }
}
