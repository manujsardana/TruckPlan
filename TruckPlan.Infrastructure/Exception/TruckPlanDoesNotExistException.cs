namespace TruckPlan.Infrastructure.Exception
{
    public class TruckPlanDoesNotExistException : System.Exception
    {
        public TruckPlanDoesNotExistException(string message) : base(message) { }
    }
}
