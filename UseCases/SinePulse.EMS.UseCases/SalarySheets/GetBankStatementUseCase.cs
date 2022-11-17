using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class GetBankStatementUseCase : IGetBankStatementUseCase
  {
    private readonly IRepository _repository;

    public GetBankStatementUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetBankStatementResponseMessage.BankStatement> GetBankStatement(GetBankStatementRequestMessage message)
    {
      var salarySheet = _repository.Table<SalarySheet>()
        .FirstOrDefault(s => s.Year == message.Year &&
                             s.Month == message.Month &&
                             s.BranchMedium.Id == message.BranchMediumId);
     
      if (salarySheet != null)
      {
        var employees = _repository.Table<SalarySheetDetail>()
          .Where(s => s.SalarySheet.Id == salarySheet.Id)
          .Select(s => s.Employee)
          .Distinct()
          .ToList();
        var employeeSalaries = GetEmployeeSalaries(employees, salarySheet.Id);
        return employeeSalaries;
      }
      return new List<GetBankStatementResponseMessage.BankStatement>();
    }

    private List<GetBankStatementResponseMessage.BankStatement> GetEmployeeSalaries(List<Domain.Employees.Employee> employees, long salarySheetId)
    {
      var employeeSalaries = new List<GetBankStatementResponseMessage.BankStatement>();
      var serialNo = 0;
      foreach (var employee in employees)
      {
        serialNo++;
        var salaryComponentIncreaseAmount = _repository.Table<SalarySheetDetail>()
          .Where(s => s.SalarySheet.Id == salarySheetId &&
                      s.Employee.Id == employee.Id &&
                      s.PayrollComponent.IncreaseDecrease == IncreaseDecreaseEnum.Increase)
          .Select(s => s.Amount)
          .ToList()
          .Sum();
        var salaryComponentDeductions = _repository.Table<SalarySheetDetail>()
          .Where(s => s.SalarySheet.Id == salarySheetId &&
                      s.Employee.Id == employee.Id &&
                      s.PayrollComponent.IncreaseDecrease == IncreaseDecreaseEnum.Decrease)
          .Select(s => s.Amount)
          .ToList()
          .Sum();
        decimal totalAmount = salaryComponentIncreaseAmount - salaryComponentDeductions;
        employeeSalaries.Add(new GetBankStatementResponseMessage.BankStatement
        {
          SerialNo = serialNo,
          EmployeeName = employee.FullName,
          AccountNumber = employee.BankAccountNo,
          Amount = totalAmount

        });
      }

      return employeeSalaries;
    }
  }
}