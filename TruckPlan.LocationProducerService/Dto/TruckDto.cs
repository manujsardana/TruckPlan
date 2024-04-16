namespace TruckPlan.LocationProducerService.Dto
{
    public class TruckDto
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; } = string.Empty;

        public EngineType TruckEngineType { get; set; } = EngineType.Unknown;

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
