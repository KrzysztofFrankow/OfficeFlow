namespace OfficeFlow.Domain.Entities
{
    public class Absences
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public int UserId { get; set; }
        public virtual Users? User { get; set; }
        public int Type { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Notes { get; set; }
        public int Status { get; set; }
    }
}
