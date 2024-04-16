    namespace TruckPlan.LocationProducerService.Dto
{
    public class RandomLocationDto
    {
        public string GeoNumber { get; set; }

        public LocationDto Nearest { get; set; }
    }

    public class LocationDto
    {
        public double Longt { get; set; }

        public double Latt { get; set; }
    }
}
