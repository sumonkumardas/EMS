using System;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.PayrollMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Payrolls
{
  public class DefineSalaryUseCase : IDefineSalaryUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public DefineSalaryUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void DefineSalary(DefineSalaryRequestMessage message)
    {
      var employee = _repository.GetById<Domain.Employees.Employee>(message.EmployeeId);
      var index = -1;
      foreach (var salaryComponent in message.SalaryComponents)
      {
        var employeeSalary = new EmployeeSalary
        {
          Amount = message.SalaryComponentAmounts[++index],
          EffectiveDate = message.EffectiveDate,
          PayrollComponent = _repository.GetById<SalaryComponent>(salaryComponent.SalaryComponentId),
          Employee = employee,
          AuditFields = new AuditFields
          {
            InsertedDateTime = DateTime.Now,
            InsertedBy = message.CurrentUserName,
            LastUpdatedBy = message.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        _repository.Insert(employeeSalary);
      }
      
      index = -1;
      foreach (var otherComponent in message.OtherComponents)
      {
        var employeeSalary = new EmployeeSalary
        {
          Amount = message.OtherComponentAmounts[++index],
          EffectiveDate = message.EffectiveDate,
          PayrollComponent = _repository.GetById<OtherComponent>(otherComponent.ComponentId),
          Employee = employee,
          AuditFields = new AuditFields
          {
            InsertedDateTime = DateTime.Now,
            InsertedBy = message.CurrentUserName,
            LastUpdatedBy = message.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        _repository.Insert(employeeSalary);
      }

      _dbContext.SaveChanges();
    }
  }
}