namespace OfficeFlow.Application.EFilesDocuments.Models
{
    public class ListModel
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string? CategoryName { get; set; }
        public int Type { get; set; }
        public string? TypeName { get; set; }
        public string? Date { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? DocumentName { get; set; }
    }
}
