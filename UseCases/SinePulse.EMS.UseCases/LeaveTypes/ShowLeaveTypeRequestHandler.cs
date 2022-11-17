using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class ShowLeaveTypeRequestHandler : IRequestHandler<ShowLeaveTypeRequestMessage, ShowLeaveTypeResponseMessage>
  {
    private readonly ShowLeaveTypeRequestMessageValidator _validator;
    private readonly IShowLeaveTypeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowLeaveTypeRequestHandler(ShowLeaveTypeRequestMessageValidator validator,
      IShowLeaveTypeUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowLeaveTypeResponseMessage> Handle(ShowLeaveTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowLeaveTypeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowLeaveTypeResponseMessage(validationResult,new LeaveTypeMessageModel());
        return Task.FromResult(returnMessage);
      }

      var model = _useCase.ShowLeaveType(request);

      returnMessage = new ShowLeaveTypeResponseMessage(validationResult,model);
      return Task.FromResult(returnMessage);
    }
  }
}