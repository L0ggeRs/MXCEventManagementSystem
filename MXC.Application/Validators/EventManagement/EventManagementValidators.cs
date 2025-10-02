using FluentValidation;
using Microsoft.Extensions.Logging;
using MXC.Application.Validators.EventManagement.Validators;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Infrastructure.Context;

namespace MXC.Application.Validators.EventManagement;

public class EventManagementValidators(
    ILogger<EventCreateValidator> eventCreateLogger,
    ILogger<EventUpdateValidator> eventUpdateLogger,
    ApplicationNoTrackingDbContext noTrackingDbContext) : IEventManagementValidators
{
    public IValidator<EventItemCreateDTO> EventCreateValidator => new EventCreateValidator(eventCreateLogger);

    public IValidator<EventItemUpdateDTO> EventUpdateValidator => new EventUpdateValidator(eventUpdateLogger, noTrackingDbContext);
}