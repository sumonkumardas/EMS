using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.TermTestMarkMessages;

namespace SinePulse.EMS.UseCases.TermTestMarks
{
  public class GetTermTestMarksRequestHandler : IRequestHandler<GetTermTestMarksRequestMessage, GetTermTestMarksResponseMessage>
  {
    private readonly GetTermTestMarksRequestMessageValidator _validator;
    private readonly IGetTermTestMarksUseCase _useCase;

    public GetTermTestMarksRequestHandler(GetTermTestMarksRequestMessageValidator validator,
      IGetTermTestMarksUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetTermTestMarksResponseMessage> Handle(GetTermTestMarksRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetTermTestMarksResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetTermTestMarksResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.GetTermTestMarks(request);

      returnMessage = new GetTermTestMarksResponseMessage(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}