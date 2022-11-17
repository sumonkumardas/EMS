using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class EditStudentUseCase : IEditStudentUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditStudentUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void UpdateStudent(EditStudentRequestMessage message)
    {
      var student = _repository.GetById<Student>(message.StudentId);

      student.Guardian = (RelationTypeEnum) message.Guardian;
      student.FullName = message.FullName;
      student.Email = message.Email;
      student.BirthDate = message.BirthDate;
      student.MobileNo = message.MobileNo;
      student.AuditFields.LastUpdatedBy = message.CurrentUserName;
      student.AuditFields.LastUpdatedDateTime = DateTime.Now;

      var studentSection = _repository.Table<StudentSection>(new []{nameof(StudentSection.Student),nameof(StudentSection.Section)}).FirstOrDefault(x=>x.Student.Id == message.StudentId);
      if (studentSection != null)
      {
        studentSection.RollNo = message.RollNo;
        studentSection.Group = message.Group;
        var section = _repository.GetById<Section>(message.SectionId);
        var @class = _repository.GetById<Class>(message.ClassId);
        studentSection.Section = section;
        studentSection.Class = @class;
      }

      _dbContext.SaveChanges();
    }
  }
}