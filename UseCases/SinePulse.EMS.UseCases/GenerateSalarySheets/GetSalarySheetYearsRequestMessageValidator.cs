using FluentValidation;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;

namespace SinePulse.EMS.UseCases.GenerateSalarySheets
{
  public class GetSalarySheetYearsRequestMessageValidator : AbstractValidator<GetSalarySheetYearsRequestMessage>
  {
    public GetSalarySheetYearsRequestMessageValidator()
    {
    }
  }
}