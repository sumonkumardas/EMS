using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ResultSheetMessages;

namespace SinePulse.EMS.UseCases.ResultSheets
{
  public class
    GetTermResultSheetRequestHandler : IRequestHandler<GetTermResultSheetRequestMessage,
      GetTermResultSheetResponseMessage>
  {
    private readonly GetTermResultSheetRequestMessageValidator _validator;
    private readonly IGetTermResultSheetUseCase _useCase;


    public GetTermResultSheetRequestHandler(GetTermResultSheetRequestMessageValidator validator,
      IGetTermResultSheetUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetTermResultSheetResponseMessage> Handle(GetTermResultSheetRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetTermResultSheetResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetTermResultSheetResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var result = _useCase.GetTermResultSheet(request);

      returnMessage = new GetTermResultSheetResponseMessage(validationResult, result);
      return Task.FromResult(returnMessage);
    }
  }
}