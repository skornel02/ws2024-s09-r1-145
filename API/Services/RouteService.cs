using Domain.Entities;
using Domain.Services;
using Org.BouncyCastle.Security;

namespace API.Services;

public class RouteService : IRouteService
{
    private const string StartRouteName = "Rajt";

    private readonly ApplicationDbContext _context;

    public RouteService(ApplicationDbContext context)
    {
        this._context = context;
    }

    public IQueryable<RouteRecord> GetRoutes()
    {
        return _context.Routes.AsQueryable();
    }

    public List<RouteRecord> SortRoutes(IEnumerable<RouteRecord> routes)
    {
        List<RouteRecord> workingCopy = routes.ToList();
        List<RouteRecord> sorted = new List<RouteRecord>();
        string nextStartLocation = StartRouteName;
        while (workingCopy.Count > 0)
        {
            var next = workingCopy.Find(route => route.StartingLocation == nextStartLocation);
            nextStartLocation = next.ArrivalLocation;
            sorted.Add(next);
            workingCopy.Remove(next);
        }
        return sorted;
    }
}
