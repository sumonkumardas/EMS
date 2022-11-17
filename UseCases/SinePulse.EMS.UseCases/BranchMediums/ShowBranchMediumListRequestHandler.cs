using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class ShowBranchMediumListRequestHandler : IRequestHandler<ShowBranchMediumListRequestMessage, ShowBranchMediumListResponseMessage>
  {
    private readonly ShowBranchMediumListRequestMessageValidator _validator;
    private readonly IShowBranchMediumListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowBranchMediumListRequestHandler(ShowBranchMediumListRequestMessageValidator validator, IShowBranchMediumListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowBranchMediumListResponseMessage> Handle(ShowBranchMediumListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowBranchMediumListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBranchMediumListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var branchMediumList = _useCase.ShowBranchMediumList(request);

      returnMessage = new ShowBranchMediumListResponseMessage(validationResult, branchMediumList);
      return Task.FromResult(returnMessage);
    }
  }
}