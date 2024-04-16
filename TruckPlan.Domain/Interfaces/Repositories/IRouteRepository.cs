namespace TruckPlan.Domain.Interfaces.Repositories
{
    public interface IRouteRepository
    {
        Task<Route> AddRouteAsync(Route route, string country);

        Task<IEnumerable<Route>> GetRoutesByTruckPlanIdAsync(int truckPlanId);
    }
}
