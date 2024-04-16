namespace TruckPlan.Domain.Interfaces.Repositories
{
    public interface IDriverReportRepository
    {
        Task<int> GetDriverReport(int age, DateTime startData, DateTime endDate, int distance, string country);
    }
}
