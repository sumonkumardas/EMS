using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAccountHeadListRequestHandler : IRequestHandler<ShowAccountHeadListRequestMessage,
    ShowAccountHeadListResponseMessage>
  {
    private readonly ShowAccountHeadListRequestMessageValidator _validator;
    private readonly IShowAccountHeadListUseCase _useCase;

    public ShowAccountHeadListRequestHandler(ShowAccountHeadListRequestMessageValidator validator,
      IShowAccountHeadListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowAccountHeadListResponseMessage> Handle(ShowAccountHeadListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowAccountHeadListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowAccountHeadListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var accountHeads = _useCase.ShowAccountHeadList(request);

      returnMessage = new ShowAccountHeadListResponseMessage(validationResult, accountHeads);
      return Task.FromResult(returnMessage);
    }
  }
}