using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class GetBranchMediumAccountHeadsRequestHandler : IRequestHandler<GetBranchMediumAccountHeadsRequestMessage,
    GetBranchMediumAccountHeadsResponseMessage>
  {
    private readonly GetBranchMediumAccountHeadsRequestMessageValidator _validator;
    private readonly IGetBranchMediumAccountHeadsUseCase _useCase;

    public GetBranchMediumAccountHeadsRequestHandler(GetBranchMediumAccountHeadsRequestMessageValidator validator,
      IGetBranchMediumAccountHeadsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetBranchMediumAccountHeadsResponseMessage> Handle(GetBranchMediumAccountHeadsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetBranchMediumAccountHeadsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetBranchMediumAccountHeadsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var list = _useCase.GetAccountHeads(request);

      returnMessage = new GetBranchMediumAccountHeadsResponseMessage(validationResult, list);
      return Task.FromResult(returnMessage);
    }
  }
}