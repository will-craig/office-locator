using OfficeLocator.DAL.Models;
namespace OfficeLocator.DAL;

public interface IOfficeService
{
    IList<Office> GetOffices();
    void SaveOffice(Office office);
}
