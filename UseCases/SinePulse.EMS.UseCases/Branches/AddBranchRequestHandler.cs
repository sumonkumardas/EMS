using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Branches
{
  public class AddBranchRequestHandler : IRequestHandler<AddBranchRequestMessage, AddBranchResponseMessage>
  {
    private readonly AddBranchRequestMessageValidator _validator;
    private readonly IAddBranchUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddBranchRequestHandler(AddBranchRequestMessageValidator validator, IAddBranchUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddBranchResponseMessage> Handle(AddBranchRequestMessage request, CancellationToken cancellationToken)
    {
      AddBranchResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBranchResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var branch = _useCase.AddBranch(request);
      _dbContext.SaveChanges();

      returnMessage = new AddBranchResponseMessage(validationResult,branch.Id);
      return Task.FromResult(returnMessage);
    }
  }
}