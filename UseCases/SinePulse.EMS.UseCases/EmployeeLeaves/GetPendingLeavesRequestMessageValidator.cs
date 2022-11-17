using FluentValidation;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class GetPendingLeavesRequestMessageValidator : AbstractValidator<GetPendingLeavesRequestMessage>
  {
    public GetPendingLeavesRequestMessageValidator()
    {
    }
  }
}