using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class
    EditAccountHeadRequestHandler : IRequestHandler<EditAccountHeadRequestMessage, EditAccountHeadResponseMessage>
  {
    private readonly EditAccountHeadRequestMessageValidator _validator;
    private readonly IEditAccountHeadUseCase _useCase;

    public EditAccountHeadRequestHandler(EditAccountHeadRequestMessageValidator validator,
      IEditAccountHeadUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditAccountHeadResponseMessage> Handle(EditAccountHeadRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditAccountHeadResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditAccountHeadResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.UpdateAccountHead(request);

      returnMessage = new EditAccountHeadResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}