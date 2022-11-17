using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class DisplayAddSalaryComponentUseCase : IDisplayAddSalaryComponentUseCase
  {
    private readonly IRepository _repository;

    public DisplayAddSalaryComponentUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayAddSalaryComponentResponseMessage DisplayAddSalaryComponent(DisplayAddSalaryComponentRequestMessage message)
    {

      var salaryComponentType = _repository.Table<SalaryComponentType>();

      var salaryComponentTypeCollection = new List<DisplayAddSalaryComponentResponseMessage.SalaryComponentType>();
      foreach (var componentType in salaryComponentType)
      {
        salaryComponentTypeCollection.Add(salaryComponentTypeEntity(componentType));
      }
      return new DisplayAddSalaryComponentResponseMessage(salaryComponentTypeCollection);
    }

    private DisplayAddSalaryComponentResponseMessage.SalaryComponentType salaryComponentTypeEntity(SalaryComponentType salaryComponentTypeEntity)
    {
      return new DisplayAddSalaryComponentResponseMessage.SalaryComponentType
      {
        ComponentTypeId = salaryComponentTypeEntity.Id,
        ComponentTypeName = salaryComponentTypeEntity.ComponentType
      };
    }
  }
}
