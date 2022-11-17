using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class EditSeatPlanUseCase : IEditSeatPlanUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditSeatPlanUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public EditSeatPlanResponseMessage EditSeatPlan(EditSeatPlanRequestMessage requestMessage)
    {
      var seatPlan = _repository.GetById<SeatPlan>(requestMessage.SeatPlanId);

      seatPlan.RollRange = requestMessage.RollRange;
      seatPlan.TotalStudent = requestMessage.TotalStudent;
      seatPlan.Status = StatusEnum.Active;
      seatPlan.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      seatPlan.AuditFields.LastUpdatedDateTime = DateTime.Now;
      seatPlan.RoomId = requestMessage.RoomId;
      seatPlan.TestId = requestMessage.TestId;

      _dbContext.SaveChanges();

      return new EditSeatPlanResponseMessage(seatPlan.Id);
    }
  }
}