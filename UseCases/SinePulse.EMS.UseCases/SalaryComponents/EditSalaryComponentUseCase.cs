using FluentValidation.Results;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Linq;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class EditSalaryComponentUseCase : IEditSalaryComponentUseCase
  {
    private readonly IRepository _repository;

    public EditSalaryComponentUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditSalaryComponent(EditSalaryComponentRequestMessage message)
    {
      var componentType = _repository.Table<SalaryComponentType>().Where(i => i.Id == message.ComponentTypeId).FirstOrDefault();
      var salaryComponent = _repository.GetById<SalaryComponent>(message.ComponentId);
      
      salaryComponent.ComponentName = message.ComponentName;
      salaryComponent.ComponentType = componentType;
      salaryComponent.AuditFields.LastUpdatedBy = message.CurrentUserName;
      salaryComponent.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}
