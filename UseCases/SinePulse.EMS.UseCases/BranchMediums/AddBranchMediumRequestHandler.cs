using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class
    AddBranchMediumRequestHandler : IRequestHandler<AddBranchMediumRequestMessage, AddBranchMediumResponseMessage>
  {
    private readonly AddBranchMediumRequestMessageValidator _validator;
    private readonly IAddBranchMediumUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddBranchMediumRequestHandler(AddBranchMediumRequestMessageValidator validator,
      IAddBranchMediumUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddBranchMediumResponseMessage> Handle(AddBranchMediumRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddBranchMediumResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBranchMediumResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var branchMedium = _useCase.AddBranchMedium(request);
      _dbContext.SaveChanges();

      returnMessage = new AddBranchMediumResponseMessage(validationResult, branchMedium.Id);
      return Task.FromResult(returnMessage);
    }
  }
}