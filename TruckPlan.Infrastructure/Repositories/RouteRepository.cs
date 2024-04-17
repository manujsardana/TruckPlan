using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces;
using TruckPlan.Domain.Interfaces.Repositories;

namespace TruckPlan.Infrastructure.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DbContext _dbContext;

        public RouteRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Route> AddRouteAsync(Route route, string country)
        {
            route = new Route(route.TruckPlanId, route.Lattitude, route.Longitude, country, route.LocationTimeStamp);

            //This should be removed when we have actual database and replace with savechangesasync
            (route as IIdGenerator).SetId(_dbContext.Routes.Count() +1);
            _dbContext.Routes.Add(route);

            return await Task.FromResult(route);
        }

        public async Task<IEnumerable<Route>> GetRoutesByTruckPlanIdAsync(int truckPlanId)
        {
            return await Task.FromResult(_dbContext.Routes.Where(x => x.TruckPlanId == truckPlanId));
        }
    }
}
