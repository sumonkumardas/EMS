using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentListRequestHandler : IRequestHandler<ShowStudentListRequestMessage, ShowStudentListResponseMessage>
  {
    private readonly ShowStudentListRequestMessageValidator _validator;
    private readonly IShowStudentListUseCase _useCase;

    public ShowStudentListRequestHandler(ShowStudentListRequestMessageValidator validator, IShowStudentListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowStudentListResponseMessage> Handle(ShowStudentListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowStudentListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowStudentListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var students = _useCase.ShowStudentsList(request);

      returnMessage = new ShowStudentListResponseMessage(validationResult, students);
      return Task.FromResult(returnMessage);
    }
  }
}