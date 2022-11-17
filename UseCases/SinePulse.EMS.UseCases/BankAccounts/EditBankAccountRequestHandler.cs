using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankAccountMessages;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class EditBankAccountRequestHandler : IRequestHandler<EditBankAccountRequestMessage, EditBankAccountResponseMessage>
  {
    private readonly EditBankAccountRequestMessageValidator _validator;
    private readonly IEditBankAccountUseCase _useCase;

    public EditBankAccountRequestHandler(EditBankAccountRequestMessageValidator validator,
      IEditBankAccountUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditBankAccountResponseMessage> Handle(EditBankAccountRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditBankAccountResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditBankAccountResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditBankAccount(request);

      returnMessage = new EditBankAccountResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}