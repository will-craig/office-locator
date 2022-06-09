namespace OfficeLocator.Models;

public class OfficeRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    public bool Wifi { get; set; }
    public bool ExtendedAccess { get; set; }
    public bool MeetingRooms { get; set; }
    public bool Kitchen { get; set; }
    public bool BreakArea { get; set; }
    public bool PetFriendly { get; set; }
    public bool Printing { get; set; }
    public bool Shower { get; set; }
}