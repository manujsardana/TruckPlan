using TruckPlan.Domain.Interfaces;

namespace TruckPlan.Domain
{
    public class TruckPlan : IIdGenerator
    {
        public int Id { get; private set; }

        public int DriverId { get; private set; }

        public int TruckId { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; } = null;

        public int Distance { get; private set; }

        public int? StartLocationId { get; private set; } = null;

        public int? EndLocationId { get; private set; } = null;

        public int GpsDeviceId { get; private set; }

        public IList<Route> Routes { get; private set; } = new List<Route>();

        public TruckPlan(int driverId, int truckId, int gpsDeviceId, DateTime startDate)
        {
            DriverId = driverId;
            TruckId = truckId;
            GpsDeviceId = gpsDeviceId;
            StartDate = startDate;
        }

        public TruckPlan AddRoute(Route route)
        {
            Routes.Add(route);
            StartDate = Routes.First().LocationTimeStamp;

            if(Routes.Count  >= 2) 
            {
                Distance += CalculateDistance(Routes[Routes.Count - 2], Routes[Routes.Count - 1]);
            }
            

            StartLocationId = Routes.First().Id;

            EndLocationId = Routes.Last().Id;
            return this;
        }

        private int CalculateDistance(Route sourceRoute, Route destinationRoute)
        {
            double rlat1 = Math.PI * sourceRoute.Lattitude / 180;
            double rlat2 = Math.PI * destinationRoute.Lattitude / 180;
            double theta = sourceRoute.Longitude - destinationRoute.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return (int)(dist * 1.609344);
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
