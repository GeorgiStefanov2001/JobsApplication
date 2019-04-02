﻿// <auto-generated />
using System;
using JobApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JobApplication.Data.Migrations
{
    [DbContext(typeof(JobApplicationDbContext))]
    partial class JobApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JobApplication.Data.Models.CV", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Education");

                    b.Property<int>("Experience");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("CVs");
                });

            modelBuilder.Entity("JobApplication.Data.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CVId");

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("Employer");

                    b.Property<string>("Name");

                    b.Property<int?>("RequiredEducation");

                    b.Property<int?>("RequiredExperience");

                    b.Property<decimal>("Salary");

                    b.HasKey("Id");

                    b.HasIndex("CVId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("JobApplication.Data.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AchievedGoals");

                    b.Property<int?>("CVId");

                    b.Property<string>("Description");

                    b.Property<string>("FutureGoals");

                    b.Property<string>("Name");

                    b.Property<string>("Technology");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CVId");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("JobApplication.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Age");

                    b.Property<string>("ConfirmPassword");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UserCreatedOn");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("Email", "Username")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL AND [Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JobApplication.Data.Models.CV", b =>
                {
                    b.HasOne("JobApplication.Data.Models.User", "User")
                        .WithOne("UserCv")
                        .HasForeignKey("JobApplication.Data.Models.CV", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JobApplication.Data.Models.Job", b =>
                {
                    b.HasOne("JobApplication.Data.Models.CV")
                        .WithMany("Jobs")
                        .HasForeignKey("CVId");
                });

            modelBuilder.Entity("JobApplication.Data.Models.Project", b =>
                {
                    b.HasOne("JobApplication.Data.Models.CV")
                        .WithMany("Projects")
                        .HasForeignKey("CVId");

                    b.HasOne("JobApplication.Data.Models.User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
