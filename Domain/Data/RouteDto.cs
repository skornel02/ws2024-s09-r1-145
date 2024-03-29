﻿namespace Domain.Data;

public class RouteDto
{
    public int Id { get; set; }
    public double Distance { get; set; }
    public string StartingLocation { get; set; }
    public string ArrivalLocation { get; set; }
    public string LocationName { get; set; }
}
