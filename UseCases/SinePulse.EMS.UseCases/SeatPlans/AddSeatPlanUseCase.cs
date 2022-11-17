using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class AddSeatPlanUseCase : IAddSeatPlanUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddSeatPlanUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public AddSeatPlanResponseMessage AddSeatPlan(AddSeatPlanRequestMessage requestMessage)
    {
      var seatPlan = new SeatPlan
      {
        RollRange = requestMessage.RollRange,
        TotalStudent = requestMessage.TotalStudent,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },

        RoomId = requestMessage.RoomId,
        TestId = requestMessage.TestId
      };

      _repository.Insert(seatPlan);

      _dbContext.SaveChanges();

      return new AddSeatPlanResponseMessage(seatPlan.Id);
    }
  }
}