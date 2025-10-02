using Microsoft.EntityFrameworkCore;
using MXC.Domain.Entities;

namespace MXC.Infrastructure.Context.EntityModelBuilder;

internal sealed class LocationEntityModelBuilder : EntityModelBuilderBase
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocationEntity>(entity =>
        {
            configureCommonProperties(entity);

            entity.Property(e => e.LocationName).HasMaxLength(100).IsRequired();
        });
    }
}