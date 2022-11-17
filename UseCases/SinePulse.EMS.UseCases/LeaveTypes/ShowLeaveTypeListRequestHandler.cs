using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class ShowLeaveTypeListRequestHandler : IRequestHandler<ShowLeaveTypeListRequestMessage, ShowLeaveTypeListResponseMessage>
  {
    private readonly ShowLeaveTypeListRequestMessageValidator _validator;
    private readonly IShowLeaveTypeListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowLeaveTypeListRequestHandler(ShowLeaveTypeListRequestMessageValidator validator,
      IShowLeaveTypeListUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowLeaveTypeListResponseMessage> Handle(ShowLeaveTypeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowLeaveTypeListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowLeaveTypeListResponseMessage(validationResult,new List<LeaveTypeMessageModel>());
        return Task.FromResult(returnMessage);
      }

      var list = _useCase.ShowLeaveTypeList(request);

      returnMessage = new ShowLeaveTypeListResponseMessage(validationResult,list);
      return Task.FromResult(returnMessage);
    }
  }
}