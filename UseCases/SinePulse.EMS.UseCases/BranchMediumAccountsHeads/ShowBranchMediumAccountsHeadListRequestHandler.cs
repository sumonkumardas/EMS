using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class ShowBranchMediumAccountsHeadListRequestHandler : IRequestHandler<ShowBranchMediumAccountsHeadListRequestMessage, ShowBranchMediumAccountsHeadListResponseMessage>
  {
    private readonly ShowBranchMediumAccountsHeadListRequestMessageValidator _validator;
    private readonly IShowBranchMediumAccountsHeadListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowBranchMediumAccountsHeadListRequestHandler(ShowBranchMediumAccountsHeadListRequestMessageValidator validator,
      IShowBranchMediumAccountsHeadListUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowBranchMediumAccountsHeadListResponseMessage> Handle(ShowBranchMediumAccountsHeadListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowBranchMediumAccountsHeadListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBranchMediumAccountsHeadListResponseMessage(validationResult,new List<BranchMediumAccountsHeadMessageModel>());
        return Task.FromResult(returnMessage);
      }

      var list = _useCase.ShowBranchMediumAccountsHeadList(request);

      returnMessage = new ShowBranchMediumAccountsHeadListResponseMessage(validationResult,list);
      return Task.FromResult(returnMessage);
    }
  }
}