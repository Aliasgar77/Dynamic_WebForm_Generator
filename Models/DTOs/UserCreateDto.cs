using System.ComponentModel.DataAnnotations;

namespace DynamicReportBuilder1.Model.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Report Name is required")]
        [MaxLength(20, ErrorMessage = "User Name cannot exceed 10 characters")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
