namespace TruckPlan.Domain.Interfaces.Services
{
    public interface ICountryFromLocationService
    {
        Task<string> GetCountryFromLocation(double lattitude, double longitude);
    }
}
