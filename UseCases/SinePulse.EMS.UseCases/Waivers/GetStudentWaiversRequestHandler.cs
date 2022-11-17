using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.WaiverMessages;

namespace SinePulse.EMS.UseCases.Waivers
{
  public class GetStudentWaiversRequestHandler : IRequestHandler<GetStudentWaiversRequestMessage, GetStudentWaiversResponseMessage>
  {
    private readonly GetStudentWaiversRequestMessageValidator _validator;
    private readonly IGetStudentWaiversUseCase _useCase;

    public GetStudentWaiversRequestHandler(GetStudentWaiversRequestMessageValidator validator, IGetStudentWaiversUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetStudentWaiversResponseMessage> Handle(GetStudentWaiversRequestMessage request, CancellationToken cancellationToken)
    {
      GetStudentWaiversResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetStudentWaiversResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var studentWaivers = _useCase.GetStudentWaivers(request);

      returnMessage = new GetStudentWaiversResponseMessage(validationResult, studentWaivers);
      return Task.FromResult(returnMessage);
    }
  }
}