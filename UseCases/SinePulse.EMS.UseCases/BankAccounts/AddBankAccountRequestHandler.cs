using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankAccountMessages;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class AddBankAccountRequestHandler : IRequestHandler<AddBankAccountRequestMessage, AddBankAccountResponseMessage>
  {
    private readonly AddBankAccountRequestMessageValidator _validator;
    private readonly IAddBankAccountUseCase _useCase;

    public AddBankAccountRequestHandler(AddBankAccountRequestMessageValidator validator, IAddBankAccountUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddBankAccountResponseMessage> Handle(AddBankAccountRequestMessage request, CancellationToken cancellationToken)
    {
      AddBankAccountResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBankAccountResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bankBranch = _useCase.AddBankAccount(request);

      returnMessage = new AddBankAccountResponseMessage(validationResult, bankBranch);
      return Task.FromResult(returnMessage);
    }
  }
}