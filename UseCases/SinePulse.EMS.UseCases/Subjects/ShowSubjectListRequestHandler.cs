using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class ShowSubjectListRequestHandler : IRequestHandler<ShowSubjectListRequestMessage, ShowSubjectListResponseMessage>
  {
    private readonly ShowSubjectListRequestMessageValidator _validator;
    private readonly IShowSubjectListUseCase _useCase;

    public ShowSubjectListRequestHandler(ShowSubjectListRequestMessageValidator validator, IShowSubjectListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowSubjectListResponseMessage> Handle(ShowSubjectListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowSubjectListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowSubjectListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var subjects = _useCase.ShowSubjectList(request);

      returnMessage = new ShowSubjectListResponseMessage(validationResult, subjects);
      return Task.FromResult(returnMessage);
    }
  }
}