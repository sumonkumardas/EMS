using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentContactPersonMessages;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class GetStudentContactPersonsRequestHandler : IRequestHandler<GetStudentContactPersonsRequestMessage, GetStudentContactPersonsResponseMessage>
  {
    private readonly GetStudentContactPersonsRequestMessageValidator _validator;
    private readonly IGetStudentContactPersonsUseCase _useCase;

    public GetStudentContactPersonsRequestHandler(GetStudentContactPersonsRequestMessageValidator validator,
      IGetStudentContactPersonsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetStudentContactPersonsResponseMessage> Handle(GetStudentContactPersonsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetStudentContactPersonsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetStudentContactPersonsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var contactPersons = _useCase.GetContactPersons(request);

      returnMessage = new GetStudentContactPersonsResponseMessage(validationResult, contactPersons);
      return Task.FromResult(returnMessage);
    }
  }
}