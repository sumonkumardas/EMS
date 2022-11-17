using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class EditEducationalQualificationRequestHandler : IRequestHandler<EditEducationalQualificationRequestMessage, EditEducationalQualificationResponseMessage>
  {
    private readonly EditEducationalQualificationRequestMessageValidator _validator;
    private readonly IEditEducationalQualificationUseCase _useCase;

    public EditEducationalQualificationRequestHandler(EditEducationalQualificationRequestMessageValidator validator, IEditEducationalQualificationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditEducationalQualificationResponseMessage> Handle(EditEducationalQualificationRequestMessage request, CancellationToken cancellationToken)
    {
      EditEducationalQualificationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditEducationalQualificationResponseMessage(validationResult, 0);
        return Task.FromResult(returnMessage);
      }

      var employeeId = _useCase.EditEducationalQualification(request);

      returnMessage = new EditEducationalQualificationResponseMessage(validationResult, employeeId);
      return Task.FromResult(returnMessage);
    }
  }
}