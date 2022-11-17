using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Linq;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class AddSalaryComponentUseCase : IAddSalaryComponentUseCase
  {
    private readonly IRepository _repository;

    public AddSalaryComponentUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public SalaryComponent AddSalaryComponent(AddSalaryComponentRequestMessage message)
    {
      var componentType = _repository.Table<SalaryComponentType>().FirstOrDefault(i => i.Id == message.ComponentTypeId);
      var salaryComponent = new SalaryComponent
      {
        ComponentName = message.ComponentName,
        ComponentType = componentType,
        IncreaseDecrease = IncreaseDecreaseEnum.Increase,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(salaryComponent);
      return salaryComponent;
    }
  }
}
