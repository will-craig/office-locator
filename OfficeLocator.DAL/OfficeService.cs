using System.Globalization;
using CsvHelper;
using OfficeLocator.DAL.Models;

namespace OfficeLocator.DAL;

public class OfficeService : IOfficeService
{
    private readonly string _csvFile = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}/test-offices.csv";

    public IList<Office> GetOffices()
    {
        List<Office> offices;

        var file = _csvFile;
        
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            offices = csv.GetRecords<Office>().ToList();
        }

        return offices;
    }

    public void SaveOffice(Office office)
    {
        var file = _csvFile;
        
        var offices = GetOffices();

        var existingOffice = offices.SingleOrDefault(o => o.Name == office.Name);

        if (existingOffice != null)
        {
            offices.Remove(existingOffice);
        }
        
        offices.Add(office);
        
        using (var writer = new StreamWriter(file))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(offices);
        }
    }
}
