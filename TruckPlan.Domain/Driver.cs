using TruckPlan.Domain.Interfaces;

namespace TruckPlan.Domain
{
    public class Driver : IIdGenerator
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public DateOnly DateOfBirth { get; private set; }

        public string NationalId { get; private set; }

        public Driver(string name, DateOnly dateOfBirth, string nationalId) 
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            NationalId = nationalId;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
