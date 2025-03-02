namespace DynamicReportBuilder1.Model.DTOs
{
    public class ReportDto
    {
        public Guid ReportId { get; set; }
        public string ReportName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ReportDescription { get; set; }
    }
}
