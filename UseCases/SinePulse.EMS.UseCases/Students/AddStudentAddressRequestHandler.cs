using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentAddressRequestHandler : IRequestHandler<AddStudentAddressRequestMessage, AddStudentAddressResponseMessage>
  {
    private readonly AddStudentAddressRequestMessageValidator _validator;
    private readonly IAddStudentAddressUseCase _useCase;

    public AddStudentAddressRequestHandler(AddStudentAddressRequestMessageValidator validator,
      IAddStudentAddressUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddStudentAddressResponseMessage> Handle(AddStudentAddressRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddStudentAddressResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddStudentAddressResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddStudentAddress(request);

      returnMessage = new AddStudentAddressResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}