using Microsoft.EntityFrameworkCore;
using MXC.Domain.Entities;
using MXC.Shared;

namespace MXC.Infrastructure.Context;

public class ApplicationNoTrackingDbContext(DbContextOptions<ApplicationNoTrackingDbContext> options) : DbContext(options)
{
    public virtual DbSet<EventEntity> Events { get; set; }

    public override int SaveChanges() => throw new InvalidOperationException("This DbContext is read-only.");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => throw new InvalidOperationException("This DbContext is read-only.");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Ensure.NotNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        ModelCreating.ModelCreatingBase(modelBuilder);
    }
}