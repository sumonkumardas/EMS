using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class EditShiftUseCase : IEditShiftUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditShiftUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void UpdateShift(EditShiftRequestMessage requestMessage)
    {
      var shift = _repository.GetById<Shift>(requestMessage.ShiftId);

      shift.ShiftName = requestMessage.ShiftName;
      shift.StartTime = requestMessage.StartTime;
      shift.EndTime = requestMessage.EndTime;
      shift.Status = requestMessage.Status;
      shift.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      shift.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
    }
  }
}