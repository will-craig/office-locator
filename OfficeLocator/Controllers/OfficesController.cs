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
        private readonly IFacilityPreferenceService _facilityService;

        public OfficesController(IOfficeService officeService, IGeolocationService locationService, IFacilityPreferenceService facilityService)
        {
            _officeService = officeService;
            _locationService = locationService;
            _facilityService = facilityService;
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
        /// Gets top 10 offices closest to the requested latitude and longitude, and returns them ordered by facility preference
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [HttpPost("/facilities", Name = "retrieveOfficesWithFacilities")]
        public IActionResult GetOfficeWithRequestedFacilities(OfficeRequest officeRequest)
        {
            var validLat = double.TryParse(officeRequest.Latitude, out double requestLatitude);
            var validLong = double.TryParse(officeRequest.Longitude, out double requestLongitude);
            if (validLat == false && validLong == false) 
                return BadRequest();
            
            var currentLocation = new Coordinates(requestLatitude, requestLongitude);
            var offices = _officeService.GetOffices();
            var closestOfficeList = GetOfficeDistances(offices, currentLocation).OrderBy(e => e.Value).Take(10).ToList();
            var orderedPreferenceList = GetOfficesByFacilityPreference(officeRequest, closestOfficeList, offices).ToList();
            return Ok(orderedPreferenceList);
        }

        private IEnumerable<WeightedOffice> GetOfficeDistances(IList<Office> offices, Coordinates currentLocation)
        {
            foreach (var office in offices)
            {
                var validLat1 = double.TryParse(office.Latitude, out double convertedLatitude);
                var validLong1 = double.TryParse(office.Longitude, out double convertedLongitude);
                if (!validLat1 || !validLong1) continue;
                var delta = _locationService.DetermineCoordinateDelta(currentLocation, new Coordinates(convertedLatitude, convertedLongitude));
                yield return new WeightedOffice(office.Name, delta);
            }
        }

        private IEnumerable<Office> GetOfficesByFacilityPreference(OfficeRequest officeRequest,
            List<WeightedOffice> closestOfficeList, IList<Office> offices)
        {
            var preferredOfficeList =
                (from closeOffice in closestOfficeList
                    select offices.First(e => e.Name == closeOffice.Name)
                    into selectedOffice
                    let count = _facilityService.PreferenceCount(officeRequest, selectedOffice)
                    select new WeightedOffice(selectedOffice.Name, count)).ToList();

            foreach (var preferredOffice in preferredOfficeList.OrderByDescending(e => e.Value))
            {
                yield return offices.First(e => e.Name == preferredOffice.Name);
            }
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
