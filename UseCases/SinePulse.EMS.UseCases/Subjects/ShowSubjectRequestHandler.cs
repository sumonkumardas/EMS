using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class ShowSubjectRequestHandler : IRequestHandler<ShowSubjectRequestMessage, ShowSubjectResponseMessage>
  {
    private readonly ShowSubjectRequestMessageValidator _validator;
    private readonly IShowSubjectUseCase _useCase;

    public ShowSubjectRequestHandler(ShowSubjectRequestMessageValidator validator, IShowSubjectUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowSubjectResponseMessage> Handle(ShowSubjectRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowSubjectResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowSubjectResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var subject = _useCase.ShowSubject(request);

      returnMessage = new ShowSubjectResponseMessage(validationResult, subject);
      return Task.FromResult(returnMessage);
    }
  }
}