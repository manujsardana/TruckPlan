namespace TruckPlan.Domain.Interfaces.Repositories
{
    public interface ITruckPlanRepository
    {
        Task<TruckPlan> AddTruckPlanAsync(TruckPlan driver);

        Task<List<TruckPlan>> GetAllTruckPlansAsync();

        Task AddRouteToTruckPlanAsync(Route route, string country);
    }
}
