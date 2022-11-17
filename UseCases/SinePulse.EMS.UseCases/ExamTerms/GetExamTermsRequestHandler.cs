using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamTermMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetExamTermsRequestHandler : IRequestHandler<GetExamTermsRequestMessage, GetExamTermsResponseMessage>
  {
    private readonly GetExamTermsRequestMessageValidator _validator;
    private readonly IGetExamTermsUseCase _useCase;

    public GetExamTermsRequestHandler(GetExamTermsRequestMessageValidator validator,
      IGetExamTermsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetExamTermsResponseMessage> Handle(GetExamTermsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetExamTermsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetExamTermsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.GetExamTerms(request);

      returnMessage = new GetExamTermsResponseMessage(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}