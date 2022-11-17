using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class GetSubjectsToAddInClassRequestHandler : IRequestHandler<GetSubjectsToAddInClassRequestMessage, GetSubjectsToAddInClassResponseMessage>
  {
    private readonly GetSubjectsToAddInClassRequestMessageValidator _validator;
    private readonly IGetSubjectsToAddInClassUseCase _useCase;

    public GetSubjectsToAddInClassRequestHandler(GetSubjectsToAddInClassRequestMessageValidator validator, IGetSubjectsToAddInClassUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetSubjectsToAddInClassResponseMessage> Handle(GetSubjectsToAddInClassRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetSubjectsToAddInClassResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetSubjectsToAddInClassResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var subjects = _useCase.GetSubjects(request);

      returnMessage = new GetSubjectsToAddInClassResponseMessage(validationResult, subjects);
      return Task.FromResult(returnMessage);
    }
  }
}