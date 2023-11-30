using Microsoft.AspNetCore.Http;

namespace OfficeFlow.Application.EFilesDocuments.Models
{
    public class ListModel
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? DocumentName { get; set; }
    }
}
