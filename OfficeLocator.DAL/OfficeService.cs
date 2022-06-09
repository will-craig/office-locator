using System.Globalization;
using CsvHelper;
using OfficeLocator.DAL.Models;

namespace OfficeLocator.DAL;

public class OfficeService : IOfficeService
{
    public IList<Office> GetOffices()
    {
        List<Office> offices;
     
        var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        var file = $"{directory}/test-offices.csv";
        
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            offices = csv.GetRecords<Office>().ToList();
        }

        return offices;
    }
}
