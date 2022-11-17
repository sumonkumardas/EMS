using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankBranchMessages;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class ShowBankBranchRequestHandler : IRequestHandler<ShowBankBranchRequestMessage, ShowBankBranchResponseMessage>
  {
    private readonly ShowBankBranchRequestMessageValidator _validator;
    private readonly IShowBankBranchUseCase _useCase;

    public ShowBankBranchRequestHandler(ShowBankBranchRequestMessageValidator validator, IShowBankBranchUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowBankBranchResponseMessage> Handle(ShowBankBranchRequestMessage request, CancellationToken cancellationToken)
    {
      ShowBankBranchResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBankBranchResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bankBranch = _useCase.GetBankBranch(request);

      returnMessage = new ShowBankBranchResponseMessage(validationResult, bankBranch);
      return Task.FromResult(returnMessage);
    }
  }
}