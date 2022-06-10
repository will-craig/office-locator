using OfficeLocator.DAL.Models;
using OfficeLocator.Models;
using OfficeLocator.Helper;
namespace OfficeLocator.Services;

public interface IFacilityPreferenceService
{
    public int PreferenceCount(OfficeRequest requested, Office existingOffice);
}
public class FacilityPreferenceService : IFacilityPreferenceService
{
    public int PreferenceCount(OfficeRequest requested, Office existingOffice)
    {
        var prefCount = 0;
        if (requested.Kitchen.IsTrue() && existingOffice.Kitchen.IsTrue()) prefCount++;
        if (requested.Printing.IsTrue() && existingOffice.Printing.IsTrue()) prefCount++;
        if (requested.Shower.IsTrue() && existingOffice.Shower.IsTrue()) prefCount++;
        if (requested.Wifi.IsTrue() && existingOffice.Wifi.IsTrue()) prefCount++;
        if (requested.BreakArea.IsTrue() && existingOffice.BreakArea.IsTrue()) prefCount++;
        if (requested.ExtendedAccess.IsTrue() && existingOffice.ExtendedAccess.IsTrue()) prefCount++;
        if (requested.MeetingRooms.IsTrue() && existingOffice.MeetingRooms.IsTrue()) prefCount++;
        if (requested.PetFriendly.IsTrue() && existingOffice.PetFriendly.IsTrue()) prefCount++;
        return prefCount;
    }
}