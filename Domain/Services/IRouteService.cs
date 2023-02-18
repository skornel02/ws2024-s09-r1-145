using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services;

public interface IRouteService
{
    IQueryable<RouteRecord> GetRoutes();

    List<RouteRecord> SortRoutes(IEnumerable<RouteRecord> routes);
}
