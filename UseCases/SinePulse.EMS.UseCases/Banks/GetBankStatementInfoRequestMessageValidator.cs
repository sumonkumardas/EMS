using FluentValidation;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class GetBankStatementInfoRequestMessageValidator : AbstractValidator<GetBankStatementInfoRequestMessage>
  {
    public GetBankStatementInfoRequestMessageValidator()
    {
    }
  }
}