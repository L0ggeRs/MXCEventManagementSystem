using FluentValidation;
using MXC.Domain.DataTransferObjects.EventManagement;

namespace MXC.Application.Validators.EventManagement;

public interface IEventManagementValidators
{
    IValidator<EventItemCreateDTO> EventCreateValidator { get; }
    IValidator<EventItemUpdateDTO> EventUpdateValidator { get; }
}