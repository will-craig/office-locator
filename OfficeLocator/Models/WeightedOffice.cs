namespace OfficeLocator.Models;

public class WeightedOffice
{
    public WeightedOffice(string name, double value)
    {
        Name = name;
        Value = value;
    }
    public string Name { get; set; }
    public double Value { get; set; }
}