using FluentValidation;
using Microsoft.Extensions.Logging;
using MXC.Domain.DataTransferObjects.EventManagement;

namespace MXC.Application.Validators.EventManagement.Validators;

public sealed class EventCreateValidator : AbstractValidator<EventItemCreateDTO>
{
    public EventCreateValidator(ILogger<EventCreateValidator> logger)
    {
        logger.LogInformation("Create event validating is starting.");

        validateBasicFields();

        logger.LogInformation("Create event validating is complete.");
    }

    private void validateBasicFields()
    {
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