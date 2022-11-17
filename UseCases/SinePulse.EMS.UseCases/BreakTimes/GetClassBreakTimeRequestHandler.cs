using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BreakTimeMessages;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public class GetClassBreakTimeRequestHandler : IRequestHandler<GetClassBreakTimeRequestMessage, GetClassBreakTimeResponseMessage>
  {
    private readonly GetClassBreakTimeRequestMessageValidator _validator;
    private readonly IGetClassBreakTimeUseCase _useCase;

    public GetClassBreakTimeRequestHandler(GetClassBreakTimeRequestMessageValidator validator,
      IGetClassBreakTimeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetClassBreakTimeResponseMessage> Handle(GetClassBreakTimeRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetClassBreakTimeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetClassBreakTimeResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var breakTimeProperties = _useCase.GetClassBreakTime(request);

      returnMessage = new GetClassBreakTimeResponseMessage(validationResult, breakTimeProperties);
      return Task.FromResult(returnMessage);
    } 
  }
}