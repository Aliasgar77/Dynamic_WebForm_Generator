using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamic_WebForm_Generator.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateStructure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormTemplateId = table.Column<int>(type: "int", nullable: false),
                    FormValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormData_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormTemplates",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name", "TemplateStructure" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Form to collect employee details", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Employee Form", "{ \"fields\": [ { \"label\": \"Employee Name\", \"type\": \"text\", \"required\": true }, { \"label\": \"Email\", \"type\": \"email\", \"required\": true } ] }" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Form to collect customer feedback", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Customer Feedback Form", "{ \"fields\": [ { \"label\": \"Customer Name\", \"type\": \"text\", \"required\": true }, { \"label\": \"Feedback\", \"type\": \"textarea\", \"required\": true } ] }" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormData_FormTemplateId",
                table: "FormData",
                column: "FormTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormData");

            migrationBuilder.DropTable(
                name: "FormTemplates");
        }
    }
}
