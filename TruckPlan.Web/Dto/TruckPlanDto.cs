namespace TruckPlan.Web.Dto
{
    public class TruckPlanDto
    {
        public int Id { get; set; }

        public int DriverId { get; set; }

        public int TruckId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } = null;

        public int Distance { get; set; }

        public int? StartLocationId { get; set; } = null;

        public int? EndLocationId { get; set; } = null;

        public int GpsDeviceId { get; set; }
    }
}
