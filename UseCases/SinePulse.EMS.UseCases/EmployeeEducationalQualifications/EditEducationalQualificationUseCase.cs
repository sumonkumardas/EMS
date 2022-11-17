using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class EditEducationalQualificationUseCase : IEditEducationalQualificationUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditEducationalQualificationUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public long EditEducationalQualification(EditEducationalQualificationRequestMessage message)
    {
      var educationalQualification =
        _repository.GetByIdWithInclude<EducationalQualification>(message.EducationalQualificationId,
          new[] {nameof(Domain.Employees.Employee)});

      educationalQualification.DegreeName = message.DegreeName;
      educationalQualification.PassingYear = message.PassingYear;
      educationalQualification.MajorSubject = message.MajorSubject;
      educationalQualification.InstituteName = message.InstituteName;
      educationalQualification.AuditFields.LastUpdatedBy = message.CurrentUserName;
      educationalQualification.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
      return educationalQualification.Employee.Id;
    }
  }
}