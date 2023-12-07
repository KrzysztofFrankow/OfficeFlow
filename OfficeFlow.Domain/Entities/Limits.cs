namespace OfficeFlow.Domain.Entities
{
    public class Limits
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public int UserId { get; set; }
        public virtual Users? User { get; set; }
        public int Type { get; set; }
        public int DaysLimit { get; set; }
    }
}
