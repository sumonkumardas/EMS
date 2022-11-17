using FluentValidation;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class SalarySheetAccountPostingRequestMessageValidator : AbstractValidator<SalarySheetAccountPostingRequestMessage>
  {
    public SalarySheetAccountPostingRequestMessageValidator()
    {
    }
  }
}