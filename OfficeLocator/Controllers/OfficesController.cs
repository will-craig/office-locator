using Microsoft.AspNetCore.Mvc;
using OfficeLocator.DAL;
using OfficeLocator.Models;
using OfficeLocator.Services;

namespace OfficeLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly IGeolocationService _locationService;

        public OfficesController(IOfficeService officeService, IGeolocationService locationService)
        {
            _officeService = officeService;
            _locationService = locationService;
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
            var currentLocation = new Coordinates(latitude, longitude);
            var offices = _officeService.GetOffices();
            var officeList = new Dictionary<string, double>();
            foreach (var office in offices)
            {
                var delta = _locationService.DetermineCoordinateDelta(currentLocation,
                    new Coordinates(office.Latitude, office.Latitude));
                officeList.Add(office.Name, delta);
            }

            var closestOffice = 
                officeList.First(office => office.Value == officeList.Min(distance => distance.Value));
            return Ok(offices.First(e => e.Name == closestOffice.Key));
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
