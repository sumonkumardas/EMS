using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class EditSalaryComponentTypeUseCase : IEditSalaryComponentTypeUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditSalaryComponentTypeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public EditSalaryComponentTypeResponseMessage EditSalaryComponentType(EditSalaryComponentTypeRequestMessage requestMessage)
    {
      var salaryComponentType = _repository.GetById<SalaryComponentType>(requestMessage.SalaryComponentTypeId);

      salaryComponentType.ComponentType = requestMessage.ComponentType;
      salaryComponentType.Status = requestMessage.Status;

      salaryComponentType.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      salaryComponentType.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();

      return new EditSalaryComponentTypeResponseMessage(salaryComponentType.Id);
    }
  }
}
