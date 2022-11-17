using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TrialBalanceMessages;

namespace SinePulse.EMS.UseCases.TrialBalances
{
  public class ShowTrialBalanceRequestHandler
    : IRequestHandler<ShowTrialBalanceRequestMessage, ValidatedData<ShowTrialBalanceResponseMessage>>
  {
    private readonly ShowTrialBalanceRequestMessageValidator _validator;
    private readonly IShowTrialBalanceUseCase _useCase;


    public ShowTrialBalanceRequestHandler(ShowTrialBalanceRequestMessageValidator validator,
      IShowTrialBalanceUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowTrialBalanceResponseMessage>> Handle(ShowTrialBalanceRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowTrialBalanceResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowTrialBalanceResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var data = _useCase.ShowTrialBalance(request);

      returnMessage = new ValidatedData<ShowTrialBalanceResponseMessage>(validationResult, data);
      return Task.FromResult(returnMessage);
    }
  }
}