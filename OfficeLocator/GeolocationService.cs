using OfficeLocator.Models;

namespace OfficeLocator;

public interface IGeolocationService
{
    public (double, double) DetermineDistance(Coordinates userLoc, Coordinates officeLoc);
}

public class GeolocationService : IGeolocationService
{
    public (double, double) DetermineDistance(Coordinates userLoc, Coordinates officeLoc)
    {
        throw new NotImplementedException();
    }
}