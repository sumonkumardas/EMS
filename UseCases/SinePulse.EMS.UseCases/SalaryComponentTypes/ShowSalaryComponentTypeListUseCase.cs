using System;
using System.Collections.Generic;
using System.Text;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class ShowSalaryComponentTypeListUseCase : IShowSalaryComponentTypeListUseCase
  {
    private readonly IRepository _repository;

    public ShowSalaryComponentTypeListUseCase(IRepository repository)
    {
      _repository = repository;
    }
    public ShowSalaryComponentTypeListResponseMessage ShowSalaryComponentTypeList(ShowSalaryComponentTypeListRequestMessage message)
    {
      var salaryComponentType = _repository.Table<SalaryComponentType>();

      var salaryComponentTypeCollection = new List<ShowSalaryComponentTypeListResponseMessage.SalaryComponentType>();
      foreach (var componentType in salaryComponentType)
      {
        salaryComponentTypeCollection.Add(salaryComponentTypeEntity(componentType));
      }
      return new ShowSalaryComponentTypeListResponseMessage(salaryComponentTypeCollection);
    }

    private ShowSalaryComponentTypeListResponseMessage.SalaryComponentType salaryComponentTypeEntity(SalaryComponentType salaryComponentTypeEntity)
    {
      return new ShowSalaryComponentTypeListResponseMessage.SalaryComponentType
      {
        Id = salaryComponentTypeEntity.Id,
        ComponentType = salaryComponentTypeEntity.ComponentType,
        Status = salaryComponentTypeEntity.Status
      };
    }
  }
}
