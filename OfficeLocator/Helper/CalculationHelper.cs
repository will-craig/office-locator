namespace OfficeLocator.Helper;

public static class CalculationHelper
{
    public static (double, double) ReturnGreatest(double userCoord, double officeCoord) {
        var first = Math.Max(userCoord, officeCoord);
        var second = Math.Min(userCoord, officeCoord);
        return (first, second);
    }
}