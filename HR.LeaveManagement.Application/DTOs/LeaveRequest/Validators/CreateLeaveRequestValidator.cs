using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestValidator : AbstractValidator<CreateLeaveRequestDto>
    {
        public CreateLeaveRequestValidator(ILeaveRequestRepository leaveRequestRepository)
        {
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await leaveRequestRepository.Exists(id);
                    return !leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}