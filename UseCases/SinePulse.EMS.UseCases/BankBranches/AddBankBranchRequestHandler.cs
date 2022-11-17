using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.UseCases.Banks;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class AddBankBranchRequestHandler : IRequestHandler<AddBankBranchRequestMessage, AddBankBranchResponseMessage>
  {
    private readonly AddBankBranchRequestMessageValidator _validator;
    private readonly IAddBankBranchUseCase _useCase;

    public AddBankBranchRequestHandler(AddBankBranchRequestMessageValidator validator, IAddBankBranchUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddBankBranchResponseMessage> Handle(AddBankBranchRequestMessage request, CancellationToken cancellationToken)
    {
      AddBankBranchResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBankBranchResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bankBranch = _useCase.AddBankBranch(request);

      returnMessage = new AddBankBranchResponseMessage(validationResult, bankBranch);
      return Task.FromResult(returnMessage);
    }
  }
}