namespace OfficeFlow.MVC.Models.Admin
{
    public class UserListItemModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; } = default(DateTime?);
    }
}
