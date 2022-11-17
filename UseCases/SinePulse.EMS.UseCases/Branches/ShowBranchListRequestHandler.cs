using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMessages;

namespace SinePulse.EMS.UseCases.Branches
{
  public class ShowBranchListRequestHandler : IRequestHandler<ShowBranchListRequestMessage, ShowBranchListResponseMessage>
  {
    private readonly ShowBranchListRequestMessageValidator _validator;
    private readonly IShowBranchListUseCase _useCase;

    public ShowBranchListRequestHandler(ShowBranchListRequestMessageValidator validator, IShowBranchListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowBranchListResponseMessage> Handle(ShowBranchListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowBranchListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBranchListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var branches = _useCase.ShowBranchList(request);

      returnMessage = new ShowBranchListResponseMessage(validationResult, branches);
      return Task.FromResult(returnMessage);
    }
  }
}