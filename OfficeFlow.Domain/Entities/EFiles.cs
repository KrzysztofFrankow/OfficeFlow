namespace OfficeFlow.Domain.Entities
{
    public class EFiles
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public int UserId { get; set; }
        public virtual Users? User { get; set; }
        public string? FolderNumber { get; set; }
        public string? StorageLocation { get; set; }
        public string? Notes { get; set; }
        public ICollection<EFileDocuments>? EFileDocuments { get; set; }
    }
}
