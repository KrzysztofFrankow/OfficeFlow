namespace OfficeFlow.Application.EFiles.Models
{
    public class ListModel
    {
        public Guid PublicId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public int UserId { get; set; }
        public string? FolderNumber { get; set; }
        public string? StorageLocation { get; set; }
    }
}
