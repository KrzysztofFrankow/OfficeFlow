namespace OfficeFlow.Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public string LastName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public UsersAddress Address { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public virtual EFiles? EFile { get; set; }
    }
}
