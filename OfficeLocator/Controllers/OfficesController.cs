using Microsoft.AspNetCore.Mvc;
using OfficeLocator.DAL;
using OfficeLocator.DAL.Models;
using OfficeLocator.Models;
using OfficeLocator.Services;

namespace OfficeLocator.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                var parsedLatitude = double.TryParse(office.Latitude, out double latitudeConverted);
                var parsedLongitude = double.TryParse(office.Longitude, out double longitudeConverted);

                if (parsedLatitude && parsedLongitude)
                {
                    var delta = _locationService.DetermineCoordinateDelta(currentLocation,
                        new Coordinates(latitudeConverted, longitudeConverted));
                    officeList.Add(office.Name, delta);
                }
            }

            var closestOffice = 
                officeList.First(office => office.Value == officeList.Min(distance => distance.Value));
            return Ok(offices.First(e => e.Name == closestOffice.Key));
        }
        
        /// <summary>
        /// Saves an office
        /// </summary>
        /// <param name="office"></param>
        /// <returns></returns>
        [HttpPost(Name = "saveOffice")]
        public IActionResult SaveOffice([FromForm] OfficeRequest office)
        {
            var officeToSave = new Office
            {
                Latitude = office.Latitude,
                Longitude = office.Longitude,
                Name = office.Name,
                Wifi = office.Wifi,
                ExtendedAccess = office.ExtendedAccess,
                MeetingRooms = office.MeetingRooms,
                Kitchen = office.Kitchen,
                BreakArea = office.BreakArea,
                PetFriendly = office.PetFriendly,
                Printing = office.Printing,
                Shower = office.Shower
            };
            
            _officeService.SaveOffice(officeToSave);
            
            return Ok(office);
        }
    }
}
