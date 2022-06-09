using NUnit.Framework;
using OfficeLocator.Models;

namespace OfficeLocator.Tests;

public class Tests
{
    private IGeolocationService geolocationService;
    public Tests()
    {
        geolocationService = new GeolocationService();
    }

    [Test]
    public void GeolocationMeasurementTest()
    {
        var userLocation = new Coordinates(-3, 7);
        var officeLocation = new Coordinates(1, 2);

        var distance = geolocationService.DetermineCoordinateDelta(userLocation, officeLocation);

        Assert.Equals(distance, 6.403);
    }
}