using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class ApproveLeaveRequestHandler : IRequestHandler<ApproveLeaveRequestMessage, ApproveLeaveResponseMessage>
  {
    private readonly ApproveLeaveRequestMessageValidator _validator;
    private readonly IApproveLeaveUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ApproveLeaveRequestHandler(ApproveLeaveRequestMessageValidator validator, IApproveLeaveUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ApproveLeaveResponseMessage> Handle(ApproveLeaveRequestMessage request, CancellationToken cancellationToken)
    {
      ApproveLeaveResponseMessage returnMessage;
      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ApproveLeaveResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var approverId = _useCase.ApproveLeave(request);
      _dbContext.SaveChanges();

      returnMessage = new ApproveLeaveResponseMessage(validationResult, approverId);
      return Task.FromResult(returnMessage);
    }
  }
}