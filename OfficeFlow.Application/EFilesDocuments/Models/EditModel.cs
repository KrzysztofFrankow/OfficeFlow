using Microsoft.AspNetCore.Http;

namespace OfficeFlow.Application.EFilesDocuments.Models
{
    public class EditModel
    {
        public int Id { get; set; }
        public string PublicId { get; set; } = default!;
        public string UserFirstName { get; set; } = default!;
        public string UserLastName { get; set; } = default!;       
        public int Category { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Notes { get; set; }
        public IFormFile? DocumentContent { get; set; }
        public string? DocumentName { get; set; }
    }
}
