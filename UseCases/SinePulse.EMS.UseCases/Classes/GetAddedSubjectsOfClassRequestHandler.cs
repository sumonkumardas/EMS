using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class GetAddedSubjectsOfClassRequestHandler : IRequestHandler<GetAddedSubjectsOfClassRequestMessage, GetAddedSubjectsOfClassResponseMessage>
  {
    private readonly GetAddedSubjectsOfClassRequestMessageValidator _validator;
    private readonly IGetAddedSubjectsOfClassUseCase _useCase;

    public GetAddedSubjectsOfClassRequestHandler(GetAddedSubjectsOfClassRequestMessageValidator validator, IGetAddedSubjectsOfClassUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetAddedSubjectsOfClassResponseMessage> Handle(GetAddedSubjectsOfClassRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetAddedSubjectsOfClassResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetAddedSubjectsOfClassResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var subjects = _useCase.GetSubjects(request);

      returnMessage = new GetAddedSubjectsOfClassResponseMessage(validationResult, subjects);
      return Task.FromResult(returnMessage);
    }
  }
}