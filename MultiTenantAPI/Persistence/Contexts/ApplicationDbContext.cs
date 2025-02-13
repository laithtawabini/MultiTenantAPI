﻿using Microsoft.EntityFrameworkCore;
using multiTenantApp.Models;
using multiTenantApp.Services;

namespace multiTenantApp.Persistence.Contexts
{
    // in a multi-database scenerio, this context manages tables that are generated in every database

    //---------------------------------- CLI COMMANDS --------------------------------------------------

    // when scaffolding database migrations, you must specify which context (ApplicationDbContext), -o is the output directory, use the following command:

    // add-migration -Context ApplicationDbContext -o Persistence/Migrations/AppDb MigrationName
    // update-database -Context ApplicationDbContext

    //--------------------------------------------------------------------------------------------------

    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentTenantService _currentTenantService;
        public string CurrentTenantId { get; set; }
        public string CurrentTenantConnectionString { get; set; }


        // Constructor 
        public ApplicationDbContext(ICurrentTenantService currentTenantService, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
            CurrentTenantConnectionString = _currentTenantService.ConnectionString;
        }

        // Application DbSets -- create for entity types to be applied to all databases
        public DbSet<Product> Products { get; set; }

        // On Configuring -- dynamic connection string, fires on every request
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string tenantConnectionString = CurrentTenantConnectionString;
            if (!string.IsNullOrEmpty(tenantConnectionString)) // use tenant db if one is specified
            {
                _ = optionsBuilder.UseSqlServer(tenantConnectionString);
            }
        }
    }
}
