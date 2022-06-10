namespace OfficeLocator.Helper;

public static class OfficeLocatorExtensions
{
    public static bool IsTrue(this string facilityProperty) => facilityProperty is "True" or "true" or "TRUE";
}