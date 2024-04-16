namespace TruckPlan.Domain.Interfaces.Repositories
{
    public interface ITruckRepository
    {
        Task<Truck> AddTruckAsync(Truck truck);

        Task<List<Truck>> GetAllTrucksAsync();
    }
}
