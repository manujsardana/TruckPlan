using TruckPlan.Domain;

namespace TruckPlan.Infrastructure
{
    //This class mimics the EF Core database with the collections. 
    //We can Add to these collections outside of DBCOntext which is not possible in EF Core but this is just for demonstration purpose.
    public class DbContext
    {
        public ICollection<Domain.TruckPlan> TruckPlans { get; private set; } = new List<Domain.TruckPlan>();

        public ICollection<Route> Routes { get; private set; } = new List<Route>();

        public ICollection<Truck> Trucks { get; private set; } = new List<Truck>();

        public  ICollection<Driver> Drivers { get; private set; } = new List<Driver>();
    }
}
