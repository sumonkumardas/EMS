using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.WorkingShiftMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.WorkingShifts
{
  public class AddWorkingShiftUseCase : IAddWorkingShiftUseCase
  {
    private readonly IRepository _repository;

    public AddWorkingShiftUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddWorkingShift(AddWorkingShiftRequestMessage requestMessage)
    {
      var workingShift = new WorkingShift
      {
        ShiftName = requestMessage.ShiftName,
        StartTime = requestMessage.StartTime,
        EndTime = requestMessage.EndTime,
        Status = requestMessage.Status,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      
      _repository.Insert(workingShift);
    }
  }
}