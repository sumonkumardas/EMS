using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAccountHeadRequestHandler : IRequestHandler<ShowAccountHeadRequestMessage, ShowAccountHeadResponseMessage>
  {
    private readonly ShowAccountHeadRequestMessageValidator _validator;
    private readonly IShowAccountHeadUseCase _useCase;

    public ShowAccountHeadRequestHandler(ShowAccountHeadRequestMessageValidator validator,
      IShowAccountHeadUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowAccountHeadResponseMessage> Handle(ShowAccountHeadRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowAccountHeadResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowAccountHeadResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var accountHead = _useCase.ShowAccountHead(request);

      returnMessage = new ShowAccountHeadResponseMessage(validationResult, accountHead);
      return Task.FromResult(returnMessage);
    }
  }
}