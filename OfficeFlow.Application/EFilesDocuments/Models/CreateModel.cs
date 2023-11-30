using Microsoft.AspNetCore.Http;

namespace OfficeFlow.Application.EFilesDocuments.Models
{
    public class CreateModel
    {
        public int Category { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Notes { get; set; }
        public IFormFile? DocumentContent { get; set; }
    }
}
