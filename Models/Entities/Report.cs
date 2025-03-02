using System.ComponentModel.DataAnnotations;

namespace DynamicReportBuilder1.Model.Entities
{
    public class Report
    {
        public Guid ReportId { get; set; } = Guid.NewGuid();
        public string ReportName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public string? ReportDescription { get; set; }

        public Guid UserId { get; set; } // Foreign Key
        public Users User { get; set; } // Navigation 

    }
}
