using Common.Utilities;
using Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
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
        modelBuilder.AddSequentialGuidForIdConvention();
        
        // Pluralizing entities tables name
        modelBuilder.AddPluralizingTableNameConvention();
    }
}