using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class AddStudentSectionRequestHandler : IRequestHandler<AddStudentSectionRequestMessage, AddStudentSectionResponseMessage>
  {
    private readonly AddStudentSectionRequestMessageValidator _validator;
    private readonly IAddStudentSectionUseCase _useCase;

    public AddStudentSectionRequestHandler(AddStudentSectionRequestMessageValidator validator, IAddStudentSectionUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddStudentSectionResponseMessage> Handle(AddStudentSectionRequestMessage request, CancellationToken cancellationToken)
    {
      AddStudentSectionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddStudentSectionResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddStudentSection(request);

      returnMessage = new AddStudentSectionResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}