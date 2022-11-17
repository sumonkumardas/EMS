using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamTermMessages;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetClassTestMarksRequestHandler : IRequestHandler<GetClassTestMarksRequestMessage, GetClassTestMarksResponseMessage>
  {
    private readonly GetClassTestMarkRequestMessageValidator _validator;
    private readonly IGetClassTestMarkUseCase _useCase;

    public GetClassTestMarksRequestHandler(GetClassTestMarkRequestMessageValidator validator,
      IGetClassTestMarkUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetClassTestMarksResponseMessage> Handle(GetClassTestMarksRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetClassTestMarksResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetClassTestMarksResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.GetClassTestMarks(request);

      returnMessage = new GetClassTestMarksResponseMessage(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}