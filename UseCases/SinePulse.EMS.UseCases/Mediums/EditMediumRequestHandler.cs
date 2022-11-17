using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MediumMessages;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class EditMediumRequestHandler : IRequestHandler<EditMediumRequestMessage, EditMediumResponseMessage>
  {
    private readonly EditMediumRequestMessageValidator _validator;
    private readonly IEditMediumUseCase _useCase;

    public EditMediumRequestHandler(EditMediumRequestMessageValidator validator, IEditMediumUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditMediumResponseMessage> Handle(EditMediumRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditMediumResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditMediumResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.UpdateMedium(request);

      returnMessage = new EditMediumResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}