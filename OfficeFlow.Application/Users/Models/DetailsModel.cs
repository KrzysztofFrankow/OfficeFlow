namespace OfficeFlow.Application.Users.Models
{
    public class DetailsModel
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public string LastName { get; set; } = default!;
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; }
        public required string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
    }
}
