using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Branches
{
  public class ShowBranchRequestHandler : IRequestHandler<ShowBranchRequestMessage, ShowBranchResponseMessage>
  {
    private readonly ShowBranchRequestMessageValidator _validator;
    private readonly IShowBranchUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowBranchRequestHandler(ShowBranchRequestMessageValidator validator, IShowBranchUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowBranchResponseMessage> Handle(ShowBranchRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowBranchResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBranchResponseMessage(validationResult, null, null);
        return Task.FromResult(returnMessage);
      }

       var branch = _useCase.ShowBranch(request);

      returnMessage = new ShowBranchResponseMessage(validationResult, branch, request.InstituteSession);
      return Task.FromResult(returnMessage);
    }
  }
}