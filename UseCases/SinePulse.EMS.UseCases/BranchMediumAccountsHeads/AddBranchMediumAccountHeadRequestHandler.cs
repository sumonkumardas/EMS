using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class AddBranchMediumAccountHeadRequestHandler : IRequestHandler<AddBranchMediumAccountHeadRequestMessage,
    AddBranchMediumAccountHeadResponseMessage>
  {
    private readonly AddBranchMediumAccountHeadRequestMessageValidator _validator;
    private readonly IAddBranchMediumAccountHeadUseCase _useCase;

    public AddBranchMediumAccountHeadRequestHandler(AddBranchMediumAccountHeadRequestMessageValidator validator,
      IAddBranchMediumAccountHeadUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddBranchMediumAccountHeadResponseMessage> Handle(AddBranchMediumAccountHeadRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddBranchMediumAccountHeadResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBranchMediumAccountHeadResponseMessage(validationResult, 0);
        return Task.FromResult(returnMessage);
      }

      var accountHead = _useCase.AddAccountHead(request);

      returnMessage = new AddBranchMediumAccountHeadResponseMessage(validationResult, accountHead.Id);
      return Task.FromResult(returnMessage);
    }
  }
}