using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BreakTimeMessages;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public class AddBreakTimeRequestHandler : IRequestHandler<AddBreakTimeRequestMessage, AddBreakTimeResponseMessage>
  {
    private readonly AddBreakTimeRequestMessageValidator _validator;
    private readonly IAddBreakTimeUseCase _useCase;

    public AddBreakTimeRequestHandler(AddBreakTimeRequestMessageValidator validator,
      IAddBreakTimeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddBreakTimeResponseMessage> Handle(AddBreakTimeRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddBreakTimeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBreakTimeResponseMessage(validationResult, 0);
        return Task.FromResult(returnMessage);
      }

      var branchMedium = _useCase.AddBreakTime(request);

      returnMessage = new AddBreakTimeResponseMessage(validationResult, branchMedium.Id);
      return Task.FromResult(returnMessage);
    }
  }
}