using System;
using System.Collections.Generic;
using System.Text;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.SalaryComponentTypes;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class ShowSalaryComponentListUseCase : IShowSalaryComponentListUseCase
  {
    private readonly IRepository _repository;

    public ShowSalaryComponentListUseCase(IRepository repository)
    {
      _repository = repository;
    }
    public ShowSalaryComponentListResponseMessage ShowSalaryComponentList(ShowSalaryComponentListRequestMessage message)
    {
      var includes = new[]
      {
        nameof(SalaryComponent.ComponentType),
      };

      var salaryComponent = _repository.Table<SalaryComponent>(includes);

      var salaryComponentCollection = new List<ShowSalaryComponentListResponseMessage.SalaryComponent>();
      foreach (var componentType in salaryComponent)
      {
        salaryComponentCollection.Add(salaryComponentEntity(componentType));
      }
      return new ShowSalaryComponentListResponseMessage(salaryComponentCollection);
    }

    private ShowSalaryComponentListResponseMessage.SalaryComponent salaryComponentEntity(SalaryComponent salaryComponentEntity)
    {
      return new ShowSalaryComponentListResponseMessage.SalaryComponent
      {
        SalaryComponentId = salaryComponentEntity.Id,
        ComponentName = salaryComponentEntity.ComponentName,
        ComponentType = new ShowSalaryComponentListResponseMessage.SalaryComponentType
        {
          SalaryComponentTypeId = salaryComponentEntity.ComponentType.Id,
          ComponentType = salaryComponentEntity.ComponentType.ComponentType
        }
      };
    }
  }
}
