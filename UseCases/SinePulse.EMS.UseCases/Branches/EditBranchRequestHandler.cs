using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Branches
{
  public class EditBranchRequestHandler : IRequestHandler<EditBranchRequestMessage, EditBranchResponseMessage>
  {
    private readonly EditBranchRequestMessageValidator _validator;
    private readonly IEditBranchUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditBranchRequestHandler(EditBranchRequestMessageValidator validator, IEditBranchUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditBranchResponseMessage> Handle(EditBranchRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditBranchResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditBranchResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditBranch(request);
      _dbContext.SaveChanges();

      returnMessage = new EditBranchResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}