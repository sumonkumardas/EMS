using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class DisplayEditSalaryComponentTypeUseCase : IDisplayEditSalaryComponentTypeUseCase
  {
    private readonly IRepository _repository;

    public DisplayEditSalaryComponentTypeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayEditSalaryComponentTypeResponseMessage ShowSalaryComponentType(DisplayEditSalaryComponentTypeRequestMessage message)
    {
      var salaryComponentType = _repository.Table<SalaryComponentType>()
        .Where(i => i.Id == message.SalaryComponentTypeId)
        .FirstOrDefault();

      var Charge = new DisplayEditSalaryComponentTypeResponseMessage.SalaryComponentType
      {
        Id = salaryComponentType.Id,
        ComponentTypeName = salaryComponentType.ComponentType,
        Status = salaryComponentType.Status
      };
      return new DisplayEditSalaryComponentTypeResponseMessage(Charge);

    }
  }
}
