namespace OfficeFlow.Application.EFiles.Models
{
    public class DetailsModel
    {
        public Guid PublicId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? FolderNumber { get; set; }
        public string? StorageLocation { get; set; }
        public string? Notes { get; set; }
        public List<EFilesDocuments.Models.ListModel>? Documents { get; set; }
    }
}
