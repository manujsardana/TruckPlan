using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Infrastructure.Exception;

namespace TruckPlan.Infrastructure.Repository
{
    public class TruckPlanRpository : ITruckPlanRepository
    {
        private readonly DbContext _dbContext;
        private readonly IRouteRepository _routeRepository;

        public TruckPlanRpository(DbContext dbContext, IRouteRepository routeRepository)
        {
            _dbContext = dbContext;
            _routeRepository = routeRepository;
        }

        public async Task AddRouteToTruckPlanAsync(Route route, string country)
        {
            var truckPlan = _dbContext.TruckPlans.FirstOrDefault(tp => tp.Id == route.TruckPlanId);

            if (truckPlan is null) throw new TruckPlanDoesNotExistException("Truck Plan for the route does not exixt");

            var addedRoute = await _routeRepository.AddRouteAsync(route, country);
            truckPlan.AddRoute(addedRoute);

        }

        public async Task<Domain.TruckPlan> AddTruckPlanAsync(Domain.TruckPlan truckPlan)
        {

            //This should be removed when we have actual database and replace with savechangesasync
            (truckPlan as IIdGenerator).SetId(_dbContext.TruckPlans.Count() + 1);
            _dbContext.TruckPlans.Add(truckPlan);

            return await Task.FromResult(truckPlan);
        }

        public async Task<List<Domain.TruckPlan>> GetAllTruckPlansAsync()
        {
            return await Task.FromResult(_dbContext.TruckPlans.ToList());
        }
    }
}
