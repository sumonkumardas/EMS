using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class AddTermTestMarksUseCase : IAddTermTestMarksUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddTermTestMarksUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AddMark(AddTermTestMarksRequestMessage requestMessage)
    {
      var test = _repository.GetById<TermTest>(requestMessage.TermTestId);
      var index = -1;
      var termTestMarks = _repository.Table<TermTestMark>()
        .Include(nameof(StudentSection) +"."+ nameof(Class))
        .Include(nameof(StudentSection) +"."+ nameof(Section))
        .Include(nameof(StudentSection) +"."+ nameof(Student))
        .Include(nameof(TermTest) + "." + nameof(ExamConfiguration))
        .Where(x => x.StudentSection.Class.Id == requestMessage.ClassId &&
                    x.StudentSection.Section.Id == requestMessage.SectionId &&
                    x.TermTest.ExamType == requestMessage.ExamType &&
                    x.TermTest.ExamConfiguration.Subject.Id == requestMessage.SubjectId);

      if (termTestMarks.Any())
      {
        foreach (var termTestMark in termTestMarks)
        {
          index++;
          termTestMark.GraceMark = requestMessage.GraceMarks[index];
          termTestMark.ObtainedMark = requestMessage.ObtainedMarks[index];
          termTestMark.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
          termTestMark.AuditFields.LastUpdatedDateTime = DateTime.Now;
          termTestMark.Remarks = requestMessage.RemarksArray[index];
        }
      }
      else
      {
        foreach (var studentSectionId in requestMessage.StudentSectionIds)
        {
          index++;
          var studentSection = _repository.GetById<StudentSection>(studentSectionId);
          var termTestMark = new TermTestMark
          {
            ObtainedMark = requestMessage.ObtainedMarks[index],
            GraceMark = requestMessage.GraceMarks[index],
            Remarks = requestMessage.RemarksArray[index],
            Status = StatusEnum.Active,

            AuditFields = new AuditFields
            {
              InsertedBy = requestMessage.CurrentUserName,
              InsertedDateTime = DateTime.Now,
              LastUpdatedBy = requestMessage.CurrentUserName,
              LastUpdatedDateTime = DateTime.Now
            },

            TermTest = test,
            StudentSection = studentSection
          };

          _repository.Insert(termTestMark);
        }
      }

      _dbContext.SaveChanges();
    }
  }
}