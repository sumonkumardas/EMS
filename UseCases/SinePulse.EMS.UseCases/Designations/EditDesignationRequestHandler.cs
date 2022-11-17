using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.DesignationMessages;

namespace SinePulse.EMS.UseCases.Designations
{
  public class EditDesignationRequestHandler : IRequestHandler<EditDesignationRequestMessage, EditDesignationResponseMessage>
  {
    private readonly EditDesignationRequestMessageValidator _validator;
    private readonly IEditDesignationUseCase _useCase;

    public EditDesignationRequestHandler(EditDesignationRequestMessageValidator validator,
      IEditDesignationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditDesignationResponseMessage> Handle(EditDesignationRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditDesignationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditDesignationResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditDesignation(request);

      returnMessage = new EditDesignationResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}