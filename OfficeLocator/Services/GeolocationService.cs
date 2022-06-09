using OfficeLocator.Helper;
using OfficeLocator.Models;

namespace OfficeLocator;

public interface IGeolocationService
{
    public double DetermineCoordinateDelta(Coordinates userLoc, Coordinates officeLoc);
}

public class GeolocationService : IGeolocationService
{
    public double DetermineCoordinateDelta(Coordinates userLoc, Coordinates officeLoc)
    {
        double CoordinateDistance(double _userLoc, double _officeLoc)
        {
            var ordered = CalculationHelper.ReturnGreatest(_userLoc, _officeLoc);
            // if one coordinate is negative, use absolute value
            if (ordered.Item1 > 0 && ordered.Item2 < 0)
            {
                return Math.Abs(ordered.Item1 - ordered.Item2);
            }
            else
                return ordered.Item1 - ordered.Item2;
        }

        var latitudeDelta = CoordinateDistance(userLoc.Latitude, officeLoc.Latitude);
        var longitudeDelta = CoordinateDistance(userLoc.Longitude, officeLoc.Longitude);

        var delta = Math.Pow(latitudeDelta, 2) + Math.Pow(longitudeDelta, 2);
        var result = Math.Sqrt(delta);
        return result;
    }

}