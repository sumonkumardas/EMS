using System;
using System.Linq;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public class AddEmployeePersonalInfoUseCase : IAddEmployeePersonalInfoUseCase
  {
    private readonly IRepository _repository;

    public AddEmployeePersonalInfoUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddEmployeePersonalInfo(AddEmployeePersonalInfoRequestMessage request)
    {
      var employee = _repository.GetById<Domain.Employees.Employee>(request.EmployeeId);
      var existingEmployeePersonalInfo =
        _repository.Table<Domain.Employees.EmployeePersonalInfo>()
          .FirstOrDefault(e => e.Employee.Id == request.EmployeeId);
      
      if (existingEmployeePersonalInfo == null)
      {
        var employeePersonalInfo = new Domain.Employees.EmployeePersonalInfo
        {
          BloodGroup = request.BloodGroup,
          FatherName = request.FatherName,
          Gender = request.Gender,
          MotherName = request.MotherName,
          Religion = request.Religion,
          SpouseName = request.SpouseName,
          Employee = employee,

          AuditFields = new AuditFields
          {
            InsertedBy = request.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = request.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        _repository.Insert(employeePersonalInfo);
      }
      else
      {
        existingEmployeePersonalInfo.BloodGroup = request.BloodGroup;
        existingEmployeePersonalInfo.FatherName = request.FatherName;
        existingEmployeePersonalInfo.Gender = request.Gender;
        existingEmployeePersonalInfo.MotherName = request.MotherName;
        existingEmployeePersonalInfo.Religion = request.Religion;
        existingEmployeePersonalInfo.SpouseName = request.SpouseName;
        existingEmployeePersonalInfo.AuditFields.InsertedBy = request.CurrentUserName;
        existingEmployeePersonalInfo.AuditFields.InsertedDateTime = DateTime.Now;
        existingEmployeePersonalInfo.AuditFields.LastUpdatedBy = request.CurrentUserName;
        existingEmployeePersonalInfo.AuditFields.LastUpdatedDateTime = DateTime.Now;
      }
    }
  }
}