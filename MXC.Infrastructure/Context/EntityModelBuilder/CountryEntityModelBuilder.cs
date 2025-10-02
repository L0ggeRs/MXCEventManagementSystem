using Microsoft.EntityFrameworkCore;
using MXC.Domain.Entities;

namespace MXC.Infrastructure.Context.EntityModelBuilder;

internal sealed class CountryEntityModelBuilder : EntityModelBuilderBase
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountryEntity>(entity =>
        {
            configureCommonProperties(entity);

            entity.Property(e => e.CountryName).IsRequired();
        });
    }
}