using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public class AssignOrChangeRoomInSectionRequestHandler : IRequestHandler<AssignOrChangeRoomInSectionRequestMessage, AssignOrChangeRoomInSectionResponseMessage>
  {
    private readonly AssignOrChangeRoomInSectionRequestMessageValidator _validator;
    private readonly IAssignOrChangeRoomInSectionUseCase _useCase;

    public AssignOrChangeRoomInSectionRequestHandler(AssignOrChangeRoomInSectionRequestMessageValidator validator, IAssignOrChangeRoomInSectionUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AssignOrChangeRoomInSectionResponseMessage> Handle(AssignOrChangeRoomInSectionRequestMessage request, CancellationToken cancellationToken)
    {
      AssignOrChangeRoomInSectionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AssignOrChangeRoomInSectionResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AssignOrChangeRoomInSection(request);

      returnMessage = new AssignOrChangeRoomInSectionResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}