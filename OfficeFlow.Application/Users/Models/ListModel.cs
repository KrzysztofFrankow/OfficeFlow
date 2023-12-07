namespace OfficeFlow.Application.Users.Models
{
    public class ListModel
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? DateOfBirth { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
    }
}
