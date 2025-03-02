﻿// <auto-generated />
using System;
using Dynamic_WebForm_Generator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dynamic_WebForm_Generator.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250228070642_AddUpdatedMigrations")]
    partial class AddUpdatedMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dynamic_WebForm_Generator.Models.FormData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FormTemplateId")
                        .HasColumnType("int");

                    b.Property<string>("FormValues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FormTemplateId");

                    b.ToTable("FormData");
                });

            modelBuilder.Entity("Dynamic_WebForm_Generator.Models.FormTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateStructure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FormTemplates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Form to collect employee details",
                            ModifiedDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Employee Form",
                            TemplateStructure = "{ \"fields\": [ { \"label\": \"Employee Name\", \"type\": \"text\", \"required\": true }, { \"label\": \"Email\", \"type\": \"email\", \"required\": true } ] }"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Form to collect customer feedback",
                            ModifiedDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Customer Feedback Form",
                            TemplateStructure = "{ \"fields\": [ { \"label\": \"Customer Name\", \"type\": \"text\", \"required\": true }, { \"label\": \"Feedback\", \"type\": \"textarea\", \"required\": true } ] }"
                        });
                });

            modelBuilder.Entity("Dynamic_WebForm_Generator.Models.FormData", b =>
                {
                    b.HasOne("Dynamic_WebForm_Generator.Models.FormTemplate", "FormTemplate")
                        .WithMany()
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormTemplate");
                });
#pragma warning restore 612, 618
        }
    }
}
