using System;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Designations
{
  public class EditDesignationUseCase : IEditDesignationUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditDesignationUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void EditDesignation(EditDesignationRequestMessage message)
    {
      var designation = _repository.GetById<Designation>(message.DesignationId);

      designation.DesignationName = message.DesignationName;
      designation.EmployeeType = message.EmployeeType;
      designation.Status = message.Status;

      designation.AuditFields.LastUpdatedBy = message.CurrentUserName;
      designation.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
    }
  }
}