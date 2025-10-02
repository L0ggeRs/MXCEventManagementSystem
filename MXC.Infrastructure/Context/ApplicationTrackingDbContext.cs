using Microsoft.EntityFrameworkCore;
using MXC.Domain.Entities;
using MXC.Domain.Interface;
using MXC.Infrastructure.Services.DateTimeService;
using MXC.Shared;

namespace MXC.Infrastructure.Context;

public class ApplicationTrackingDbContext(DbContextOptions<ApplicationTrackingDbContext> options, IDateTimeService dateTimeService) : DbContext(options)
{
    public virtual DbSet<EventEntity> Events { get; set; }
    public virtual DbSet<LocationEntity> Locations { get; set; }
    public virtual DbSet<CountryEntity> Countries { get; set; }

    public override int SaveChanges()
    {
        setModificationMetadata();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        setModificationMetadata();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Ensure.NotNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        ModelCreating.ModelCreatingBase(modelBuilder);
    }

    private void setModificationMetadata()
    {
        var time = dateTimeService.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IBaseModificationDataEntity>()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
            .ToList())
        {
            entry.Entity.LastModified = time;
        }
    }
}