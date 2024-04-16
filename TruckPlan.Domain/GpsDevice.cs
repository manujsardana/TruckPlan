using System.ComponentModel.DataAnnotations;

namespace TruckPlan.Domain
{
    public class GpsDevice
    {
        public int Id { get; private set; }

        [Required]
        public string Manufacturer { get; private set; } = string.Empty;

        public GpsDeviceType DeviceType { get; private set; } = GpsDeviceType.Unknown;
    }

    public enum GpsDeviceType
    {
        Wired,
        Wireless,
        Unknown
    }
}
