using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.JobTypeMessages;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class EditJobTypeRequestHandler : IRequestHandler<EditJobTypeRequestMessage, EditJobTypeResponseMessage>
  {
    private readonly EditJobTypeRequestMessageValidator _validator;
    private readonly IEditJobTypeUseCase _useCase;

    public EditJobTypeRequestHandler(EditJobTypeRequestMessageValidator validator,
      IEditJobTypeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditJobTypeResponseMessage> Handle(EditJobTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditJobTypeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditJobTypeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditJobType(request);

      returnMessage = new EditJobTypeResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}