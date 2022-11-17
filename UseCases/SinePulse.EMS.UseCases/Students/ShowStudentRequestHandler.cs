using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentRequestHandler
    : IRequestHandler<ShowStudentRequestMessage, ValidatedData<ShowStudentResponseMessage>>
  {
    private readonly ShowStudentRequestMessageValidator _validator;
    private readonly IShowStudentUseCase _useCase;

    public ShowStudentRequestHandler(ShowStudentRequestMessageValidator validator,
      IShowStudentUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowStudentResponseMessage>> Handle(ShowStudentRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowStudentResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowStudentResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowStudent(request);

      returnMessage = new ValidatedData<ShowStudentResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}