using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class
    GetStudentAddressRequestHandler : IRequestHandler<GetStudentAddressRequestMessage, GetStudentAddressResponseMessage>
  {
    private readonly GetStudentAddressRequestMessageValidator _validator;
    private readonly IGetStudentAddressUseCase _useCase;

    public GetStudentAddressRequestHandler(GetStudentAddressRequestMessageValidator validator,
      IGetStudentAddressUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetStudentAddressResponseMessage> Handle(GetStudentAddressRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetStudentAddressResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetStudentAddressResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var studentAddress = _useCase.GetStudentAddress(request);

      returnMessage = new GetStudentAddressResponseMessage(validationResult, studentAddress);
      return Task.FromResult(returnMessage);
    }
  }
}