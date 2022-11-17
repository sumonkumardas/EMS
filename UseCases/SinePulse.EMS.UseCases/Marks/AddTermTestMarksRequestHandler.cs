using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public class AddTermTestMarksRequestHandler : IRequestHandler<AddTermTestMarksRequestMessage, AddTermTestMarksResponseMessage>
  {
    private readonly AddTermTestMarksRequestMessageValidator _validator;
    private readonly IAddTermTestMarksUseCase _useCase;

    public AddTermTestMarksRequestHandler(AddTermTestMarksRequestMessageValidator validator,
      IAddTermTestMarksUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddTermTestMarksResponseMessage> Handle(AddTermTestMarksRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddTermTestMarksResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddTermTestMarksResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddMark(request);

      returnMessage = new AddTermTestMarksResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}