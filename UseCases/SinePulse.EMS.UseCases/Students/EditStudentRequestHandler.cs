using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class EditStudentRequestHandler : IRequestHandler<EditStudentRequestMessage, EditStudentResponseMessage>
  {
    private readonly EditStudentRequestMessageValidator _validator;
    private readonly IEditStudentUseCase _useCase;

    public EditStudentRequestHandler(EditStudentRequestMessageValidator validator, IEditStudentUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditStudentResponseMessage> Handle(EditStudentRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditStudentResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditStudentResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.UpdateStudent(request);

      returnMessage = new EditStudentResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}