using System;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class AddSalaryComponentTypeUseCase : IAddSalaryComponentTypeUseCase
  {
    private readonly IRepository _repository;

    public AddSalaryComponentTypeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public SalaryComponentType AddSalaryComponentType(AddSalaryComponentTypeRequestMessage message)
    {
      
      var salaryComponentType = new SalaryComponentType
      {
        ComponentType = message.ComponentType,
        Status = Domain.Enums.StatusEnum.Active,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(salaryComponentType);
      return salaryComponentType;
    }
  }
}
