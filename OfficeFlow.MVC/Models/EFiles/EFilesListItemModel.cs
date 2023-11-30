namespace OfficeFlow.MVC.Models.EFiles
{
    public class EFilesListItemModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; } = default(DateTime?);
    }
}
