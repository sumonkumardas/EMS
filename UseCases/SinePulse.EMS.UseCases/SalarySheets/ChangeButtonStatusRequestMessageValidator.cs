using FluentValidation;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class ChangeButtonStatusRequestMessageValidator : AbstractValidator<ChangeButtonStatusRequestMessage>
  {
    public ChangeButtonStatusRequestMessageValidator()
    {
    }
  }
}