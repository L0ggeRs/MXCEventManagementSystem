using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Infrastructure.Context;

namespace MXC.Application.Validators.EventManagement.Validators;

public sealed class EventUpdateValidator : AbstractValidator<EventItemUpdateDTO>
{
    private readonly ApplicationNoTrackingDbContext _noTrackingDbContext;

    public EventUpdateValidator(ILogger<EventUpdateValidator> logger, ApplicationNoTrackingDbContext noTrackingDbContext)
    {
        _noTrackingDbContext = noTrackingDbContext;

        logger.LogInformation("Update event validating is starting.");

        validateBasicFields();

        logger.LogInformation("Update event validating is complete.");
    }

    private void validateBasicFields()
    {
        RuleFor(x => x.EventId)
            .MustAsync(async (eventId, cancellationToken) => await _noTrackingDbContext.Events.AnyAsync(e => e.Id == eventId, cancellationToken));

        RuleFor(x => x.EventName)
            .NotEmpty();

        RuleFor(x => x.LocationId)
            .NotEmpty();

        When(x => x.Capacity.HasValue, () =>
        {
            RuleFor(x => x.Capacity!.Value)
                .Must(x => x > 0);
        });
    }
}