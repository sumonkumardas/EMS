using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class AddBankRequestHandler : IRequestHandler<AddBankRequestMessage, AddBankResponseMessage>
  {
    private readonly AddBankRequestMessageValidator _validator;
    private readonly IAddBankUseCase _useCase;

    public AddBankRequestHandler(AddBankRequestMessageValidator validator, IAddBankUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddBankResponseMessage> Handle(AddBankRequestMessage request, CancellationToken cancellationToken)
    {
      AddBankResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBankResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bank = _useCase.AddBank(request);

      returnMessage = new AddBankResponseMessage(validationResult, bank);
      return Task.FromResult(returnMessage);
    }
  }
}