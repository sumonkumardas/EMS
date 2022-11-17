using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class AddLeaveTypeRequestHandler : IRequestHandler<AddLeaveTypeRequestMessage, AddLeaveTypeResponseMessage>
  {
    private readonly AddLeaveTypeRequestMessageValidator _validator;
    private readonly IAddLeaveTypeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddLeaveTypeRequestHandler(AddLeaveTypeRequestMessageValidator validator,
      IAddLeaveTypeUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddLeaveTypeResponseMessage> Handle(AddLeaveTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddLeaveTypeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddLeaveTypeResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var leaveTpe = _useCase.AddLeaveType(request);
      _dbContext.SaveChanges();

      returnMessage = new AddLeaveTypeResponseMessage(validationResult, leaveTpe.Id);
      return Task.FromResult(returnMessage);
    }
  }
}