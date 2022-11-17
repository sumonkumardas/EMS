using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentPromotionMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class DisplayPromoteStudentsPageUseCase : IDisplayPromoteStudentsPageUseCase
  {
    private readonly IRepository _repository;

    public DisplayPromoteStudentsPageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayPromoteStudentsPageResponseMessage DisplayPromoteStudentsPage(
      DisplayPromoteStudentsPageRequestMessage requestMessage)
    {
      var alreadyPromotedStudentIds = _repository.Filter<StudentSection>(
          x => x.Section.Session.Id == requestMessage.NextSessionId)
        .AsEnumerable().Select(x => x.StudentId)
        .ToImmutableHashSet();
      var resultPublishedStudentIds = _repository.Filter<ResultSheet, StudentSection>(
          x => x.StudentSection.Section.Id == requestMessage.CurrentSectionId,
          x => x.StudentSection)
        .AsEnumerable().Select(x => x.StudentSection.StudentId)
        .ToImmutableHashSet();
      var currentSectionStudents = _repository.Filter<StudentSection, Student>(
          x => x.Section.Id == requestMessage.CurrentSectionId,
          x => x.Student).AsEnumerable().Select(ToResponseSection).ToList()
        .Where(x => !alreadyPromotedStudentIds.Contains(x.StudentId) &&
                    resultPublishedStudentIds.Contains(x.StudentId))
        .ToList();
      var nextSessionSections = _repository
        .Filter<Section, ICollection<StudentSection>>(
          x => x.Session.Id == requestMessage.NextSessionId &&
               x.ClassId == requestMessage.NextClassId,
          x => x.StudentSections)
        .AsEnumerable()
        .Select(ToResponseSection).ToList();

      return new DisplayPromoteStudentsPageResponseMessage
      {
        NextSessionSections = nextSessionSections,
        CurrentSectionStudents = currentSectionStudents
      };
    }

    private DisplayPromoteStudentsPageResponseMessage.Student ToResponseSection(StudentSection studentSection)
    {
      return new DisplayPromoteStudentsPageResponseMessage.Student
      {
        StudentId = studentSection.StudentId,
        StudentName = studentSection.Student.FullName,
        StudentRoll = studentSection.RollNo,
        StudentSectionId = studentSection.Id
      };
    }

    private DisplayPromoteStudentsPageResponseMessage.Section ToResponseSection(Section section)
    {
      return new DisplayPromoteStudentsPageResponseMessage.Section
      {
        SectionId = section.Id,
        SectionName = section.SectionName,
        AvailableSeats = section.NumberOfStudents - section.StudentSections.Count
      };
    }
  }
}