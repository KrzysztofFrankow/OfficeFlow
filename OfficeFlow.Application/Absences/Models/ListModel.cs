namespace OfficeFlow.Application.Absences.Models
{
    public class ListModel
    {
        public Guid PublicId { get; set; }
        public string? UserName { get; set; }
        public int Type { get; set; }
        public string? TypeName { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? DateCreated { get; set; }
        public string? StatusName { get; set; }
        public int Status { get; set; }
    }
}
