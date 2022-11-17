using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamTermMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetExamTermMarksRequestHandler : IRequestHandler<GetExamTermMarksRequestMessage, GetExamTermMarksResponseMessage>
  {
    private readonly GetExamTermMarksRequestMessageValidator _validator;
    private readonly IGetExamTermMarksUseCase _useCase;

    public GetExamTermMarksRequestHandler(GetExamTermMarksRequestMessageValidator validator,
      IGetExamTermMarksUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetExamTermMarksResponseMessage> Handle(GetExamTermMarksRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetExamTermMarksResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetExamTermMarksResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.GetExamTermMarks(request);

      returnMessage = new GetExamTermMarksResponseMessage(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}