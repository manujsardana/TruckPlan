namespace TruckPlan.Web.Dto
{
    public class TruckDto
    {

        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public EngineType TruckEngineType { get; set; }

        public int GpsDeviceId { get; set; }

    }

    public enum EngineType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid,
        Unknown
    }
}