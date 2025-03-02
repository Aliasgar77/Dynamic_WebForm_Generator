using System.ComponentModel.DataAnnotations;

namespace DynamicReportBuilder1.Model.DTOs
{
    public class ReportCreateDto
    {
        [Required(ErrorMessage ="Report Name is required")]
        [MaxLength(100, ErrorMessage ="Report Name cannot exceed 100 characters")]
        public string ReportName { get; set; }

        [MaxLength(500, ErrorMessage ="Description cannot exceed 500 characters")]
        public string? ReportDescription { get; set; }
    }
}
