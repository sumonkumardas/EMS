using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class AddAccountHeadRequestHandler : IRequestHandler<AddAccountHeadRequestMessage,
    AddAccountHeadResponseMessage>
  {
    private readonly AddAccountHeadRequestMessageValidator _validator;
    private readonly IAddAccountHeadUseCase _addAccountHeadUseCase;

    public AddAccountHeadRequestHandler(AddAccountHeadRequestMessageValidator validator,
      IAddAccountHeadUseCase addAccountHeadUseCase)
    {
      _validator = validator;
      _addAccountHeadUseCase = addAccountHeadUseCase;
    }

    public Task<AddAccountHeadResponseMessage> Handle(AddAccountHeadRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddAccountHeadResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddAccountHeadResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var accountHead = _addAccountHeadUseCase.AddAccountHead(request);

      returnMessage = new AddAccountHeadResponseMessage(validationResult, accountHead.Id);
      return Task.FromResult(returnMessage);
    }
  }
}