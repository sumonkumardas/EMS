using System;
using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class AddMarkUseCase : IAddMarkUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddMarkUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public AddMarkResponseMessage AddMark(AddMarkRequestMessage requestMessage)
    {
      var test = _repository.GetById<ClassTest>(requestMessage.TestId);
      var studentSection = _repository.GetById<StudentSection>(requestMessage.StudentSectionId);
      var existIngCtMark = _repository.Table<ClassTestMark>(new[] {nameof(ClassTestMark.ClassTest),nameof(ClassTestMark.StudentSection)}).FirstOrDefault(x => x.ClassTest.Id == test.Id && x.StudentSection.Id == studentSection.Id);

      if (existIngCtMark != null)
      {
        existIngCtMark.ObtainedMark = requestMessage.ObtainedMark;
        _dbContext.SaveChanges();
        return new AddMarkResponseMessage(existIngCtMark.Id);
      }
      else
      {
        var mark = new ClassTestMark
        {
          ObtainedMark = requestMessage.ObtainedMark,
          GraceMark = requestMessage.GraceMark,
          Remarks = requestMessage.Comment,
          Status = StatusEnum.Active,

          AuditFields = new AuditFields
          {
            InsertedBy = requestMessage.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = requestMessage.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          },

          ClassTest = test,
          StudentSection = studentSection
        };

        _repository.Insert(mark);

        _dbContext.SaveChanges();

        return new AddMarkResponseMessage(mark.Id);
      }

      
    }
  }
}