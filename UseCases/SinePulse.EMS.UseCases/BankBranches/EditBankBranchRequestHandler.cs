using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankBranchMessages;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class EditBankBranchRequestHandler : IRequestHandler<EditBankBranchRequestMessage, EditBankBranchResponseMessage>
  {
    private readonly EditBankBranchRequestMessageValidator _validator;
    private readonly IEditBankBranchUseCase _useCase;

    public EditBankBranchRequestHandler(EditBankBranchRequestMessageValidator validator, IEditBankBranchUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditBankBranchResponseMessage> Handle(EditBankBranchRequestMessage request, CancellationToken cancellationToken)
    {
      EditBankBranchResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditBankBranchResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditBankBranch(request);

      returnMessage = new EditBankBranchResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}