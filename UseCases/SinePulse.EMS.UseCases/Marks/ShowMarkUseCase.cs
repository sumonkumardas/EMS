using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class ShowMarkUseCase : IShowMarkUseCase
  {
    private readonly IRepository _repository;

    public ShowMarkUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowMarkResponseMessage ShowMark(ShowMarkRequestMessage message)
    {
      var includes = new[]
      {
        nameof(ClassTestMark.ClassTest),
        nameof(ClassTestMark.StudentSection),
        $"{nameof(ClassTestMark.StudentSection)}.{nameof(StudentSection.Student)}",
        $"{nameof(ClassTestMark.StudentSection)}.{nameof(StudentSection.Section)}",
        $"{nameof(ClassTestMark.StudentSection)}.{nameof(StudentSection.Section)}.{nameof(Section.Class)}",
        $"{nameof(ClassTestMark.StudentSection)}.{nameof(StudentSection.Section)}.{nameof(Section.BranchMedium)}",
        $"{nameof(ClassTestMark.StudentSection)}.{nameof(StudentSection.Section)}.{nameof(Section.BranchMedium)}.{nameof(BranchMedium.Shift)}"
      };
      var markEntity = _repository.GetById<ClassTestMark>(message.MarkId, includes);
      return new ShowMarkResponseMessage(MarkEntity(markEntity));
    }

    private ShowMarkResponseMessage.Mark MarkEntity(ClassTestMark classTestMarkEntity)
    {
      return new ShowMarkResponseMessage.Mark
      {
        Id = classTestMarkEntity.Id,
        ObtainedMark = classTestMarkEntity.ObtainedMark,
        GraceMark = classTestMarkEntity.GraceMark,
        Comment = classTestMarkEntity.Remarks,
        Status = ConvertToMessageStatus(classTestMarkEntity.Status),
        Test = new ShowMarkResponseMessage.Test
        {
          TestId = classTestMarkEntity.ClassTest.Id,
          TestName = classTestMarkEntity.ClassTest.ExamConfiguration.Title()
        },
        Student = new ShowMarkResponseMessage.Student
        {
          StudentSectionId = classTestMarkEntity.StudentSection.Section.Id,
          StudentName = $"{classTestMarkEntity.StudentSection.Student.FullName}",
          RollNo = classTestMarkEntity.StudentSection.RollNo,
          ClassName = classTestMarkEntity.StudentSection.Section.Class.ClassName,
          ShiftName = classTestMarkEntity.StudentSection.Section.BranchMedium.Shift.ShiftName,
          SectionName = classTestMarkEntity.StudentSection.Section.SectionName
        }
      };
    }

    private static Messages.Model.Enums.StatusEnum ConvertToMessageStatus(StatusEnum entityStatus)
    {
      switch (entityStatus)
      {
        case StatusEnum.Active:
          return Messages.Model.Enums.StatusEnum.Active;
        case StatusEnum.Inactive:
          return Messages.Model.Enums.StatusEnum.Inactive;
        case StatusEnum.Pending:
          return Messages.Model.Enums.StatusEnum.Pending;
        case StatusEnum.Transferred:
          return Messages.Model.Enums.StatusEnum.Transferred;
        case StatusEnum.Passed:
          return Messages.Model.Enums.StatusEnum.Passed;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityStatus), entityStatus, null);
      }
    }
  }
}