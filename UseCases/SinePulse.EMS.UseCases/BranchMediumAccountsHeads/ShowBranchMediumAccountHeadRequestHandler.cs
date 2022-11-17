using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class ShowBranchMediumAccountHeadRequestHandler : IRequestHandler<ShowBranchMediumAccountHeadRequestMessage,
    ShowBranchMediumAccountHeadResponseMessage>
  {
    private readonly ShowBranchMediumAccountHeadRequestMessageValidator _validator;
    private readonly IShowBranchMediumAccountHeadUseCase _useCase;

    public ShowBranchMediumAccountHeadRequestHandler(ShowBranchMediumAccountHeadRequestMessageValidator validator,
      IShowBranchMediumAccountHeadUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowBranchMediumAccountHeadResponseMessage> Handle(ShowBranchMediumAccountHeadRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowBranchMediumAccountHeadResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBranchMediumAccountHeadResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var acchead = _useCase.GetAccountHead(request);

      returnMessage = new ShowBranchMediumAccountHeadResponseMessage(validationResult, acchead);
      return Task.FromResult(returnMessage);
    }
  }
}