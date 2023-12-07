namespace OfficeFlow.Application.Absences.Models
{
    public class UserLimitsModel
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string? UserName { get; set; }
        public List<LimitsListModel>? Limits { get; set; }
    }

    public class LimitsListModel
    {
        public Guid PublicId { get; set; }
        public int Type { get; set; }
        public string? TypeName { get; set; }
        public int DaysLimit { get; set; }
    }
}
