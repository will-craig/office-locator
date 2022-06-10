using OfficeLocator.Helper;
using OfficeLocator.Models;

namespace OfficeLocator.Services;

public interface IGeolocationService
{
    public double DetermineCoordinateDelta(Coordinates userLoc, Coordinates officeLoc);
}

public class GeolocationService : IGeolocationService
{
    public double DetermineCoordinateDelta(Coordinates userLocation, Coordinates officeLocation)
    {
        double CoordinateDistance(double userLoc, double officeLoc)
        {
            var (greater, lower) = CalculationHelper.ReturnGreatestThenLowest(userLoc, officeLoc);
            // if one coordinate is negative, use absolute value
            if (greater > 0 && lower < 0)
            {
                return Math.Abs(greater - lower);
            }
            return greater - lower;
        }

        var latitudeDelta = CoordinateDistance(userLocation.Latitude, officeLocation.Latitude);
        var longitudeDelta = CoordinateDistance(userLocation.Longitude, officeLocation.Longitude);
        
        return Math.Sqrt(Math.Pow(latitudeDelta, 2) + Math.Pow(longitudeDelta, 2));
    }
}