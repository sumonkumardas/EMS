using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class DisplayEditSalaryComponentUseCase : IDisplayEditSalaryComponentUseCase
  {
    private readonly IRepository _repository;

    public DisplayEditSalaryComponentUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayEditSalaryComponentResponseMessage DisplayEditSalaryComponent(DisplayEditSalaryComponentRequestMessage message)
    {
      var includes = new[] { nameof(SalaryComponent.ComponentType) };

      var salaryComponentType = _repository.Table<SalaryComponentType>();

      var salaryComponent = _repository.Table<SalaryComponent>(includes)
                                       .Where(i => i.Id == message.SalaryComponentId).FirstOrDefault();

      var salaryComponentTypeCollection = new List<DisplayEditSalaryComponentResponseMessage.SalaryComponentType>();
      
      foreach (var componentType in salaryComponentType)
      {
        salaryComponentTypeCollection.Add(salaryComponentTypeEntity(componentType));
      }
      var responseMessage = new DisplayEditSalaryComponentResponseMessage(salaryComponentTypeCollection);
      responseMessage.SalaryComponentId = salaryComponent.Id;
      responseMessage.ComponentName = salaryComponent.ComponentName;
      responseMessage.ComponentTypeId = salaryComponent.ComponentType.Id;

      return responseMessage;
    }

    private DisplayEditSalaryComponentResponseMessage.SalaryComponentType salaryComponentTypeEntity(SalaryComponentType salaryComponentTypeEntity)
    {
      return new DisplayEditSalaryComponentResponseMessage.SalaryComponentType
      {
        ComponentTypeId = salaryComponentTypeEntity.Id,
        ComponentTypeName = salaryComponentTypeEntity.ComponentType
      };
    }
  }
}
