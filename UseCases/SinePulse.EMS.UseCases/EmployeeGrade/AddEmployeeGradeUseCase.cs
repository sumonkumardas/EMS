using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class AddEmployeeGradeUseCase : IAddEmployeeGradeUseCase
  {
    private readonly IRepository _repository;

    public AddEmployeeGradeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Grade AddEmployeeGrade(AddEmployeeGradeRequestMessage message)
    {
      var grade = new Grade
      {
        GradeTitle = message.GradeTitle,
        MaxSalary = message.MaxSalary,
        MinSalary = message.MinSalary,
        Status = (Domain.Enums.StatusEnum)message.Status,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(grade);
      return grade;
    }
  }
}