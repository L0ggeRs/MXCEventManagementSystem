using Microsoft.EntityFrameworkCore;
using MXC.Shared;

namespace MXC.Infrastructure.Context;

internal static class ModelCreating
{
    internal static void ModelCreatingBase(ModelBuilder modelBuilder)
    {
        Ensure.NotNull(modelBuilder);

        modelBuilder
            .HasDefaultSchema("MXC")
            .UseCollation("Hungarian_100_CI_AS");

        new EntityModelBuilder.EventEntityModelBuilder().ConfigureModel(modelBuilder);
    }
}