using System.Linq;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public class GetEmployeePersonalInfoUseCase : IGetEmployeePersonalInfoUseCase
  {
    private readonly IRepository _repository;

    public GetEmployeePersonalInfoUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetEmployeePersonalInfoResponseMessage.EmployeePersonalInfo GetEmployeePersonalInfo(
      GetEmployeePersonalInfoRequestMessage message)
    {
      var employee = _repository.GetByIdWithInclude<Domain.Employees.Employee>(message.EmployeeId,
        new[] {nameof(Domain.Employees.Employee.ReportTo)});
      var employeePersonalInfo = _repository.Table<Domain.Employees.EmployeePersonalInfo>()
        .FirstOrDefault(e => e.Employee.Id == message.EmployeeId);
      var requestEmployeePersonalInfo = ToRequestEmployeePersonalInfo(employee, employeePersonalInfo);
      return requestEmployeePersonalInfo;
    }

    private GetEmployeePersonalInfoResponseMessage.EmployeePersonalInfo ToRequestEmployeePersonalInfo(
      Domain.Employees.Employee employee, Domain.Employees.EmployeePersonalInfo employeePersonalInfo)
    {
      var requestEmployeePersonalInfo = new GetEmployeePersonalInfoResponseMessage.EmployeePersonalInfo();
      if (employee != null)
      {
        requestEmployeePersonalInfo.DOB = employee.DOB;
        requestEmployeePersonalInfo.NationalId = employee.NationalId;
        requestEmployeePersonalInfo.JoiningDate = employee.JoiningDate;
        requestEmployeePersonalInfo.EmailAddress = employee.EmailAddress;
        requestEmployeePersonalInfo.EmployeeType = employee.EmployeeType.ToString("G");
        requestEmployeePersonalInfo.BankAccountNo = employee.BankAccountNo;
        requestEmployeePersonalInfo.MobileNo = employee.MobileNo;
        if (employee.ReportTo != null)
        {
          requestEmployeePersonalInfo.ReportTo = employee.ReportTo.FullName;
          requestEmployeePersonalInfo.ReportToEmployeeId = employee.ReportTo.Id;
        }
      }

      if (employeePersonalInfo != null)
      {
        requestEmployeePersonalInfo.BloodGroup = employeePersonalInfo.BloodGroup.ToString("G");
        requestEmployeePersonalInfo.FathersName = employeePersonalInfo.FatherName;
        requestEmployeePersonalInfo.MothersName = employeePersonalInfo.MotherName;
        requestEmployeePersonalInfo.SpouseName = employeePersonalInfo.SpouseName;
        requestEmployeePersonalInfo.Religion = employeePersonalInfo.Religion.ToString("G");
        requestEmployeePersonalInfo.ReligionEnum = employeePersonalInfo.Religion;
        requestEmployeePersonalInfo.Gender = employeePersonalInfo.Gender.ToString("G");
        requestEmployeePersonalInfo.GenderEnum = employeePersonalInfo.Gender;
        requestEmployeePersonalInfo.BloodGroupEnum = employeePersonalInfo.BloodGroup;
      }

      return requestEmployeePersonalInfo;
    }
  }
}