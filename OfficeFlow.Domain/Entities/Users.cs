namespace OfficeFlow.Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public required string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public required string LastName { get; set; } = default!;
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; } = default!;
        public UsersAddress Address { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public virtual EFiles? EFile { get; set; }
        public required string PasswordHash { get; set; } 
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Absences>? Absences { get; set; }
        public virtual ICollection<Limits>? Limits { get; set; }
    }
}
