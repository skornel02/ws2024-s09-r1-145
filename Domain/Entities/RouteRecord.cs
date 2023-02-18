namespace Domain.Entities;

public class RouteRecord
{
    public int Id { get; set; }
    public double Distance { get; set; }
    public string StartingLocation { get; set; }
    public string ArrivalLocation { get; set; }
    public string RoutePartName { get; set; }
}
