using FluentValidation;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class GetBankStatementRequestMessageValidator: AbstractValidator<GetBankStatementRequestMessage>
  {
    public GetBankStatementRequestMessageValidator(){}
  }
}