using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class GetOptionalSubjectListRequestHandler : IRequestHandler<GetOptionalSubjectListRequestMessage, GetOptionalSubjectListResponseMessage>
  {
    private readonly GetOptionalSubjectListRequestMessageValidator _validator;
    private readonly IGetOptionalSubjectListUseCase _useCase;

    public GetOptionalSubjectListRequestHandler(GetOptionalSubjectListRequestMessageValidator validator, IGetOptionalSubjectListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetOptionalSubjectListResponseMessage> Handle(GetOptionalSubjectListRequestMessage request, CancellationToken cancellationToken)
    {
      GetOptionalSubjectListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetOptionalSubjectListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var subjects = _useCase.GetOptionalSubjectList(request);

      returnMessage = new GetOptionalSubjectListResponseMessage(validationResult, subjects);
      return Task.FromResult(returnMessage);
    }
  }
}