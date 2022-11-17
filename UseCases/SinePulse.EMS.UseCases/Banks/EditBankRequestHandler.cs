using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class EditBankRequestHandler : IRequestHandler<EditBankRequestMessage, EditBankResponseMessage>
  {
    private readonly EditBankRequestMessageValidator _validator;
    private readonly IEditBankUseCase _useCase;

    public EditBankRequestHandler(EditBankRequestMessageValidator validator, IEditBankUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditBankResponseMessage> Handle(EditBankRequestMessage request, CancellationToken cancellationToken)
    {
      EditBankResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditBankResponseMessage(validationResult, 0);
        return Task.FromResult(returnMessage);
      }

      var bankId = _useCase.EditBank(request);

      returnMessage = new EditBankResponseMessage(validationResult, bankId);
      return Task.FromResult(returnMessage);
    }
  }
}