namespace TruckPlan.Web.Dto
{
    public class RouteDto
    {
        public int Id { get; set; }

        public int TruckPlanId { get; set; }

        public string Country { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }

        public DateTime LocationTimeStamp { get; set; }
    }
}
