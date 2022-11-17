using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class EditLeaveTypeRequestHandler : IRequestHandler<EditLeaveTypeRequestMessage, EditLeaveTypeResponseMessage>
  {
    private readonly EditLeaveTypeRequestMessageValidator _validator;
    private readonly IEditLeaveTypeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditLeaveTypeRequestHandler(EditLeaveTypeRequestMessageValidator validator,
      IEditLeaveTypeUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditLeaveTypeResponseMessage> Handle(EditLeaveTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditLeaveTypeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditLeaveTypeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditLeaveType(request);
      _dbContext.SaveChanges();

      returnMessage = new EditLeaveTypeResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}