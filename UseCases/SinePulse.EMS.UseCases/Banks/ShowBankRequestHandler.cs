using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class ShowBankRequestHandler : IRequestHandler<ShowBankRequestMessage, ShowBankResponseMessage>
  {
    private readonly ShowBankRequestMessageValidator _validator;
    private readonly IShowBankUseCase _useCase;

    public ShowBankRequestHandler(ShowBankRequestMessageValidator validator, IShowBankUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowBankResponseMessage> Handle(ShowBankRequestMessage request, CancellationToken cancellationToken)
    {
      ShowBankResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBankResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bank = _useCase.GetBank(request);

      returnMessage = new ShowBankResponseMessage(validationResult, bank);
      return Task.FromResult(returnMessage);
    }
  }
}