namespace OfficeFlow.Application.EFiles.Models
{
    public class CreateModel
    {
        public Guid PublicId { get; set; }
        public int UserId { get; set; }
        public string? FolderNumber { get; set; }
        public string? StorageLocation { get; set; }
        public string? Notes { get; set; }

    }
}
