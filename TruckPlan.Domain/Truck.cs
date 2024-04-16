using TruckPlan.Domain.Interfaces;

namespace TruckPlan.Domain
{
    public class Truck : IIdGenerator
    {
        public int Id { get; private set; }

        public string Manufacturer { get; private set; } 

        public EngineType TruckEngineType { get; private set; }

        public int GpsDeviceId { get; private set; }

        public Truck(string manufacturer,  EngineType truckEngineType, int gpsDeviceId)
        {
            Manufacturer = manufacturer;
            TruckEngineType = truckEngineType;
            GpsDeviceId = gpsDeviceId;
        }

        public void SetId(int id)
        {
            Id = id;
        }
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
