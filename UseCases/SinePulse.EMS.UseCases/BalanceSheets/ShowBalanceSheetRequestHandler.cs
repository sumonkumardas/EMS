using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BalanceSheetMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.BalanceSheets
{
  public class ShowBalanceSheetRequestHandler
    : IRequestHandler<ShowBalanceSheetRequestMessage, ValidatedData<ShowBalanceSheetResponseMessage>>
  {
    private readonly ShowBalanceSheetRequestMessageValidator _validator;
    private readonly IShowBalanceSheetUseCase _useCase;


    public ShowBalanceSheetRequestHandler(ShowBalanceSheetRequestMessageValidator validator,
      IShowBalanceSheetUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowBalanceSheetResponseMessage>> Handle(ShowBalanceSheetRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowBalanceSheetResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowBalanceSheetResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var data = _useCase.ShowBalanceSheet(request);

      returnMessage = new ValidatedData<ShowBalanceSheetResponseMessage>(validationResult, data);
      return Task.FromResult(returnMessage);
    }
  }
}