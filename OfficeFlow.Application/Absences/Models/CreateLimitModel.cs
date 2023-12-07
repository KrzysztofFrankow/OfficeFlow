namespace OfficeFlow.Application.Absences.Models
{
    public class CreateLimitModel
    {
        public Guid PublicId { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public int DaysLimit { get; set; }
    }
}
