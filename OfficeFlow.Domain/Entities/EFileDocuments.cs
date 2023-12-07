namespace OfficeFlow.Domain.Entities
{
    public class EFileDocuments
    {
        public int Id { get; set; }
        public int EFileId { get; set; }
        public virtual EFiles? EFile { get; set; }
        public int Category { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Notes { get; set; }
        public byte[]? DocumentContent { get; set; }
        public string? DocumentContentType { get; set; }
        public string? DocumentName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
    }
}
