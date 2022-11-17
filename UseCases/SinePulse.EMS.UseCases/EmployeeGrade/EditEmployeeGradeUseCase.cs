using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class EditEmployeeGradeUseCase : IEditEmployeeGradeUseCase
  {
    private readonly IRepository _repository;

    public EditEmployeeGradeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditEmployeeGrade(EditEmployeeGradeRequestMessage message)
    {
      var grade = _repository.GetById<Grade>(message.Id);

      grade.GradeTitle = message.GradeTitle;
      grade.MaxSalary = message.MaxSalary;
      grade.MinSalary = message.MinSalary;
      grade.Status = (Domain.Enums.StatusEnum)message.Status;
      grade.AuditFields.LastUpdatedBy = message.CurrentUserName;
      grade.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}