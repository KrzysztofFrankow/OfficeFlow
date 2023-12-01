namespace OfficeFlow.Application.EFilesDocuments.Models
{
    public class DetailsModel
    {
        public int Id { get; set; }
        public string PublicId { get; set; } = default!;
        public string UserFirstName { get; set; } = default!;
        public string UserLastName { get; set; } = default!;
        public int Category { get; set; }
        public int Type { get; set; }
        public string? Date { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? Notes { get; set; }
        public string? DocumentName { get; set; }
    }
}
