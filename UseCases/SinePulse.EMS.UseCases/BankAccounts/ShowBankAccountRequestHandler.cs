using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankAccountMessages;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class ShowBankAccountRequestHandler : IRequestHandler<ShowBankAccountRequestMessage, ShowBankAccountResponseMessage>
  {
    private readonly ShowBankAccountRequestMessageValidator _validator;
    private readonly IShowBankAccountUseCase _useCase;

    public ShowBankAccountRequestHandler(ShowBankAccountRequestMessageValidator validator, IShowBankAccountUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowBankAccountResponseMessage> Handle(ShowBankAccountRequestMessage request, CancellationToken cancellationToken)
    {
      ShowBankAccountResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBankAccountResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bankAccountInfo = _useCase.ShowBankAccount(request);

      returnMessage = new ShowBankAccountResponseMessage(validationResult, bankAccountInfo);
      return Task.FromResult(returnMessage);
    }
  }
}