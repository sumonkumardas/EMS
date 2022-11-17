using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class EditJobTypeUseCase : IEditJobTypeUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditJobTypeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void EditJobType(EditJobTypeRequestMessage message)
    {
      var jobType = _repository.GetById<JobType>(message.JobTypeId);

      jobType.JobTitle = message.JobTitle;
      jobType.Status = message.Status;
      jobType.HasOverTime = message.HasOverTime;

      jobType.AuditFields.LastUpdatedBy = message.CurrentUserName;
      jobType.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
    }
  }
}