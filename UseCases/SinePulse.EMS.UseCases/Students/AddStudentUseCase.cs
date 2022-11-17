using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentUseCase : IAddStudentUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private const int StudentIdLength = 6;

    public AddStudentUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public AddStudentResponseMessage AddStudent(AddStudentRequestMessage requestMessage)
    {
      var branchMedium = _repository.GetByIdWithInclude<BranchMedium>(requestMessage.BranchMediumId, 
        new []{nameof(BranchMedium.Sessions)});
      var branchMediumCurrentSession = branchMedium.Sessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && 
                                                                                 s.EndDate >= DateTime.Now);
      var student = new Student
      {
        FullName = requestMessage.FullName,
        BirthDate = requestMessage.BirthDate,
        Email = requestMessage.Email,
        MobileNo = requestMessage.MobileNo,
        Status = StatusEnum.Active,
        StudentId = GetStudentId(),
        Guardian = requestMessage.Guardian,
        Session = branchMediumCurrentSession,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        } ,
        BranchMedium = branchMedium
      };

      _repository.Insert(student);

      var studentSection = new StudentSection
      {
        Student =  student,
        RollNo = requestMessage.RollNo,
        Class = _repository.GetById<Class>(requestMessage.ClassId),
        Group = requestMessage.Group,
        Section = _repository.GetById<Section>(requestMessage.SectionId),
        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      
      _repository.Insert(studentSection);
      _dbContext.SaveChanges();

      return new AddStudentResponseMessage(student.Id);
    }

    private string GetStudentId()
    {
      var key = StringGenerator.GenerateUniqueNumberKey(StudentIdLength);

      while (IsStudentIdExist(key))
      {
        key = StringGenerator.GenerateUniqueNumberKey(StudentIdLength);
      }
      return key;
    }
    private bool IsStudentIdExist(string studentId)
    {
      return _dbContext.Students.Any(s => s.StudentId == studentId);
    }
  }
}