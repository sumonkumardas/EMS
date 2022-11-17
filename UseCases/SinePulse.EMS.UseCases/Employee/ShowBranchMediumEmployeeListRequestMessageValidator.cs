using FluentValidation;
using SinePulse.EMS.Messages.EmployeeMessages;
namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowBranchMediumEmployeeListRequestMessageValidator : AbstractValidator<ShowBranchMediumEmployeeListRequestMessage>
  {
    public ShowBranchMediumEmployeeListRequestMessageValidator()
    {

    }
  }
}
