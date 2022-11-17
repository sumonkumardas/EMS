using FluentValidation;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class GetEmployeeLeavesRequestMessageValidator : AbstractValidator<GetEmployeeLeavesRequestMessage>
  {
  }
}