using Dynamic_WebForm_Generator.Models;

using Microsoft.EntityFrameworkCore;

namespace Dynamic_WebForm_Generator.Data
{
   
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<FormData> FormData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed predefined form templates
            modelBuilder.Entity<FormTemplate>().HasData(
       new FormTemplate
       {
           Id = 1,
           Name = "Employee Form",
           Description = "Form to collect employee details",
           TemplateStructure = "{ \"fields\": [ { \"label\": \"Employee Name\", \"type\": \"text\", \"required\": true }, { \"label\": \"Email\", \"type\": \"email\", \"required\": true } ] }",
           CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), // ✅ Hardcoded DateTime
           ModifiedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) // ✅ Hardcoded DateTime
       },
       new FormTemplate
       {
           Id = 2,
           Name = "Customer Feedback Form",
           Description = "Form to collect customer feedback",
           TemplateStructure = "{ \"fields\": [ { \"label\": \"Customer Name\", \"type\": \"text\", \"required\": true }, { \"label\": \"Feedback\", \"type\": \"textarea\", \"required\": true } ] }",
           CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), // ✅ Hardcoded DateTime
           ModifiedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) // ✅ Hardcoded DateTime
       }
   );
        }
    }
}
