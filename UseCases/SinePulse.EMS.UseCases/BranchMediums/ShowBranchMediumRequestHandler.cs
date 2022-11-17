using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class
    ShowBranchMediumRequestHandler : IRequestHandler<ShowBranchMediumRequestMessage, ShowBranchMediumResponseMessage>
  {
    private readonly ShowBranchMediumRequestMessageValidator _validator;
    private readonly IShowBranchMediumUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowBranchMediumRequestHandler(ShowBranchMediumRequestMessageValidator validator,
      IShowBranchMediumUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowBranchMediumResponseMessage> Handle(ShowBranchMediumRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowBranchMediumResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBranchMediumResponseMessage(validationResult, null, null, null,null);
        return Task.FromResult(returnMessage);
      }

      var branchMedium = _useCase.ShowBranchMedium(request);

      returnMessage = new ShowBranchMediumResponseMessage(validationResult, branchMedium, request.InstituteSessions, 
        request.Institute,request.CurrentSession);
      return Task.FromResult(returnMessage);
    }
  }
}