using API.Services;
using Domain.Entities;
using Domain.Services;

namespace Test;

public class RouteServiceTest
{
    [Fact]
    public void TestRouteSorting()
    {
        var routeStart = new RouteRecord()
        {
            StartingLocation = "Rajt",
            ArrivalLocation = "Test1"
        };
        var routeMiddle = new RouteRecord()
        {
            StartingLocation = "Test1",
            ArrivalLocation = "Test2"
        };
        var routeEnd = new RouteRecord()
        {
            StartingLocation = "Test2",
            ArrivalLocation = "Test3"
        };

        IRouteService service = new RouteService(null!);
        var routesUnsorted = new RouteRecord[] { routeMiddle, routeEnd, routeStart };
        var routesPreSorted = new List<RouteRecord>() { routeStart, routeMiddle, routeEnd };
        
        var routesSorted = service.SortRoutes(routesUnsorted);
        Assert.Equal(3, routesSorted.Count);
        Assert.Same(routesSorted[0], routesPreSorted[0]);
        Assert.Same(routesSorted[1], routesPreSorted[1]);
        Assert.Same(routesSorted[2], routesPreSorted[2]);
    }
}