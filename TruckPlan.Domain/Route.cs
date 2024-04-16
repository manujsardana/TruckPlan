using System.ComponentModel.DataAnnotations;
using TruckPlan.Domain.Interfaces;

namespace TruckPlan.Domain
{
    public class Route : IIdGenerator
    {
        public int Id { get; private set; }

        public int TruckPlanId { get; private set;}

        public string Country { get; private set; }

        public double Lattitude { get; private set; }

        public double Longitude { get; private set; }

        public DateTime LocationTimeStamp { get; private set; }

        public Route(int truckPlanId, double lattitude, double longitude, string country, DateTime locationTimeStamp)
        {
            TruckPlanId = truckPlanId;
            Lattitude = lattitude;
            Longitude = longitude;
            Country = country;
            LocationTimeStamp = locationTimeStamp;
        }

        // Usually this operation is done by database. SHould be removed with actual database.
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
