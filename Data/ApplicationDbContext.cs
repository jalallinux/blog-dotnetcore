using Common.Utilities;
using Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data;

public class ApplicationDbContext: DbContext
{
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext()
    {
    }
    
    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=test;Username=postgres;Password=postgres;");
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=test;User=sa;Password=Pa55w0rd;TrustServerCertificate=true;");
        // base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntity).Assembly;
        
        // Register all entities implemented IEntity to DBContext
        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        
        // Register all FluentApi entity type configuration
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        
        // Change Cascade delete to Restrict. if parent has any child, can't delete parent
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        
        // Change Guid ID column type default value to NEWSEQUENTIALID 
        // modelBuilder.AddSequentialGuidForIdConvention();
        
        // Pluralizing entities tables name
        modelBuilder.AddPluralizingTableNameConvention();
    }
}