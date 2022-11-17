using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class AddEmployeeEducationalQualificationUseCase : IAddEmployeeEducationalQualificationUseCase
  {
    private readonly IRepository _repository;

    public AddEmployeeEducationalQualificationUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddEmployeeEducationalQualification(AddEmployeeEducationalQualificationRequestMessage request)
    {
      var employee = _repository.GetById<SinePulse.EMS.Domain.Employees.Employee>(request.EmployeeId);

      var employeeEducationalQualification = new SinePulse.EMS.Domain.Employees.EducationalQualification
      {
        InstituteName = request.InstituteName,
        DegreeName = request.DegreeName,
        MajorSubject = request.MajorSubject,
        PassingYear = request.PassingYear,
        Employee = employee,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(employeeEducationalQualification);
    }
  }
}