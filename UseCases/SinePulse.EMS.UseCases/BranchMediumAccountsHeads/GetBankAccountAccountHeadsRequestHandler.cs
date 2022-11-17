using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class GetBankAccountAccountHeadsRequestHandler : IRequestHandler<GetBankAccountAccountHeadsRequestMessage,
    GetBankAccountAccountHeadsResponseMessage>
  {
    private readonly GetBankAccountAccountHeadsRequestMessageValidator _validator;
    private readonly IGetBankAccountAccountHeadsUseCase _useCase;

    public GetBankAccountAccountHeadsRequestHandler(GetBankAccountAccountHeadsRequestMessageValidator validator,
      IGetBankAccountAccountHeadsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetBankAccountAccountHeadsResponseMessage> Handle(GetBankAccountAccountHeadsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetBankAccountAccountHeadsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetBankAccountAccountHeadsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var accountHeads = _useCase.GetBankAccountAccountHeads(request);

      returnMessage = new GetBankAccountAccountHeadsResponseMessage(validationResult, accountHeads);
      return Task.FromResult(returnMessage);
    }
  }
}