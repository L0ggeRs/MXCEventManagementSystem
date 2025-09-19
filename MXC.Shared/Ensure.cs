using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MXC.Shared;

public static class Ensure
{
    public static void NotNull(
        [NotNull] object? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}