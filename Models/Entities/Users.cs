namespace DynamicReportBuilder1.Model.Entities
{
    public class Users
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property for related reports
        public ICollection<Report> Reports { get; set; }
    }
}
