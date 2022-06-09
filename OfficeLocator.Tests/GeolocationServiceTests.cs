using System;
using NUnit.Framework;
using OfficeLocator.Models;
using OfficeLocator.Services;

namespace OfficeLocator.Tests;

public class Tests
{
    private IGeolocationService geolocationService;
    public Tests()
    {
        geolocationService = new GeolocationService();
    }

    [TestCase(-3, 7,1,2,6.403)]
    [TestCase(51.47979149889159, -0.6634434709947902, 51.4776636734289, -0.6448293669663475,0.019)]
    public void GeolocationMeasurementTest(double userLat, double userLong, double offLat, double offLong, double expectedResult)
    {
        var userLocation = new Coordinates(userLat, userLong);
        var officeLocation = new Coordinates(offLat, offLong);

        var distance = geolocationService.DetermineCoordinateDelta(userLocation, officeLocation);
        var roundedResult = Math.Round(distance, 3);
        
        Assert.AreEqual( expectedResult, roundedResult);
    }
    
}