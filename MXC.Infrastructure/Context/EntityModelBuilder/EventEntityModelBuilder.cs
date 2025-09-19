using Microsoft.EntityFrameworkCore;
using MXC.Domain.Entities;

namespace MXC.Infrastructure.Context.EntityModelBuilder;

internal sealed class EventEntityModelBuilder : EntityModelBuilderBase
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventEntity>(entity =>
        {
            configureCommonProperties(entity);

            entity.Property(e => e.EventName).IsRequired();
            entity.Property(e => e.EventLocation).HasMaxLength(100).IsRequired();
        });
    }
}