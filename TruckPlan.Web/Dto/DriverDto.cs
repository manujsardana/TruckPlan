namespace TruckPlan.Web.Dto
{
    public class DriverDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string NationalId { get; set; }
    }
}
