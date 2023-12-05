namespace OfficeFlow.Application.Absences.Models
{
    public class ListModel
    {
        public Guid PublicId { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
