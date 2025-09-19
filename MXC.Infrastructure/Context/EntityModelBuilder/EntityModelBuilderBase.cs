using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MXC.Domain.Entities;

namespace MXC.Infrastructure.Context.EntityModelBuilder;

internal abstract class EntityModelBuilderBase
{
    private readonly string _dateTimeType = "datetime";

    protected void configureCommonProperties<T>(EntityTypeBuilder<T> entity) where T : class
    {
        entity.Property(nameof(BaseEntity.LastModified)).HasColumnType(_dateTimeType);
    }
}