using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentSectionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class AddStudentSectionUseCase : IAddStudentSectionUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddStudentSectionUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AddStudentSection(AddStudentSectionRequestMessage message)
    {
      var student = _repository.GetById<Student>(message.StudentId);
      var section = _repository.GetById<Section>(message.SectionId);

      var studentSection = new StudentSection
      {
        Student = student,
        Section = section,
        RollNo = message.RollNo,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(studentSection);
      _dbContext.SaveChanges();
    }
  }
}