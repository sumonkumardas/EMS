using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class
    EditBranchMediumRequestHandler : IRequestHandler<EditBranchMediumRequestMessage, EditBranchMediumResponseMessage>
  {
    private readonly EditBranchMediumRequestMessageValidator _validator;
    private readonly IEditBranchMediumUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditBranchMediumRequestHandler(EditBranchMediumRequestMessageValidator validator,
      IEditBranchMediumUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditBranchMediumResponseMessage> Handle(EditBranchMediumRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditBranchMediumResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditBranchMediumResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditBranchMedium(request);
      _dbContext.SaveChanges();

      returnMessage = new EditBranchMediumResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}