using FluentValidation;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class GetSalarySheetRequestMessageValidator : AbstractValidator<GetSalarySheetRequestMessage>
  {
    public GetSalarySheetRequestMessageValidator()
    {
    }
  }
}