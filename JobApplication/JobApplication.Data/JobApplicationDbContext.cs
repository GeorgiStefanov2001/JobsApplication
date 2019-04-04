using JobApplication.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Data
{
    /// <summary>
    /// This is the DbContext for the database that this application uses.
    /// </summary>
    public class JobApplicationDbContext : DbContext
    {
        public JobApplicationDbContext(DbContextOptions<JobApplicationDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// This method checks if the optionsBuilder is not already configured and 
        /// it is not, configures it by connencting to the database via connection string.
        /// </summary>
        /// <param name="optionsBuilder">optionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationData.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// This method calls the OnModelCreating method of the DbContext class that is inherited.
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
