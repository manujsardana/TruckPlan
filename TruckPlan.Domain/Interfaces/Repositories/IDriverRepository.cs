namespace TruckPlan.Domain.Interfaces.Repositories
{
    public interface IDriverRepository
    {
        Task<Driver> AddDriverAsync(Driver driver);

        Task<List<Driver>> GetAllDriversAsync();
    }
}
