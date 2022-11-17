using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class DisplayAddMarkPageUseCase : IDisplayAddMarkPageUseCase
  {
    private readonly IRepository _repository;

    public DisplayAddMarkPageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayAddMarkPageResponseMessage DisplayAddMarkPage(
      DisplayAddMarkPageRequestMessage requestMessage)
    {
      var responseMessage = new DisplayAddMarkPageResponseMessage(new List<DisplayAddMarkPageResponseMessage.Student>(),
        new List<DisplayAddMarkPageResponseMessage.Student>(),
        new List<DisplayAddMarkPageResponseMessage.Term>(), new List<DisplayAddMarkPageResponseMessage.Subject>(),
        new List<DisplayAddMarkPageResponseMessage.ExamType>(),
        new List<DisplayAddMarkPageResponseMessage.ClassTest>());

      if (requestMessage.TestId != 0)
      {
        var includes = new[]
          {
        nameof(StudentSection.Student),
        nameof(StudentSection.Section),
        $"{nameof(StudentSection.Section)}.{nameof(Section.Class)}",
        $"{nameof(StudentSection.Section)}.{nameof(Section.BranchMedium)}",
        $"{nameof(StudentSection.Section)}.{nameof(Section.BranchMedium)}.{nameof(BranchMedium.Shift)}"
      };
        var classTest = _repository.GetById<ClassTest, Session>(
          requestMessage.TestId,
          x => x.ExamConfiguration.Term.Session);

        var section = GetSection(requestMessage);
        var condition = section == null
          ? s => true
          : (Expression<Func<StudentSection, bool>>)(s => s.Section.Id == section.Id);
        var studentSections = _repository.Filter(condition,
          x => x.Student,
          x => x.Section,
          x => x.Section.Class,
          x => x.Section.BranchMedium,
          x => x.Section.BranchMedium.Shift).ToList();
        var studentList = new List<DisplayAddMarkPageResponseMessage.Student>();
        foreach (var studentSection in studentSections)
        {
          studentList.Add(ToRequestObject(studentSection,classTest.FullMarks));
        }

        long sessionId = classTest.ExamConfiguration.Term.Session.Id;
        var allStudentIds = _repository.Filter<SubjectChoice, Student>(x =>
            x.Subject.Id == requestMessage.SubjectId && x.Session.Id == sessionId, x => x.Student)
          .AsEnumerable().Select(x => x.Student.Id).ToList();
        Expression<Func<StudentSection, bool>> otherSectionStudentFilterCondition =
          x => allStudentIds.Contains(x.Student.Id) && x.Section.Id != section.Id;
        var otherSectionStudents = _repository.Filter(otherSectionStudentFilterCondition,
            x => x.Student,
            x => x.Section,
            x => x.Section.Class,
            x => x.Section.BranchMedium,
            x => x.Section.BranchMedium.Shift).AsEnumerable().Select(s => ToRequestObject(s, classTest.FullMarks))
          .ToList();
        
        responseMessage = new DisplayAddMarkPageResponseMessage(studentList, otherSectionStudents, 
          new List<DisplayAddMarkPageResponseMessage.Term>(), new List<DisplayAddMarkPageResponseMessage.Subject>(),
          new List<DisplayAddMarkPageResponseMessage.ExamType>(), new List<DisplayAddMarkPageResponseMessage.ClassTest>()); 
      }


      if (requestMessage.SessionId != null && requestMessage.TermId == null && requestMessage.SubjectId == null && requestMessage.ExamType == null)
      {
        var termIncludes = new[]
        {
          nameof(ExamTerm.Session)
        };
        var termList = _repository.Table<ExamTerm>(termIncludes)
          .Where(x => x.Session.Id == requestMessage.SessionId.Value);

        responseMessage = new DisplayAddMarkPageResponseMessage(new List<DisplayAddMarkPageResponseMessage.Student>(),
          new List<DisplayAddMarkPageResponseMessage.Student>(),
          termList.Select(ToRequestTerm).ToList(), new List<DisplayAddMarkPageResponseMessage.Subject>(),
          new List<DisplayAddMarkPageResponseMessage.ExamType>(),
          new List<DisplayAddMarkPageResponseMessage.ClassTest>());
      }

      if (requestMessage.SessionId != null && requestMessage.TermId != null && requestMessage.SubjectId == null && requestMessage.ExamType == null)
      {
        var subjectIncludes = new[]
        {
          nameof(ClassTest.ExamConfiguration),
          nameof(ClassTest.ExamConfiguration)+"."+nameof(ClassTest.ExamConfiguration.Term),
          nameof(ClassTest.ExamConfiguration)+"."+nameof(ClassTest.ExamConfiguration.Subject)
        };
        var classTestList = _repository.Table<ClassTest>(subjectIncludes);

        var classId = _repository.GetById<Section>(requestMessage.SectionId, new[] { nameof(Section.Class) }).Class.Id;
        var group = _repository.GetById<Section>(requestMessage.SectionId, new[] { nameof(Section.Class) }).Group;

        var classSubjects = _repository.Table<ClassSubject>()
          .Where(x => x.Class.Id == classId &&
                      x.Group == group)
          .Select(x => x.Subject)
          .ToList();

        var examConfigSubjects = classTestList.Select(t => t.ExamConfiguration)
          .Where(e => e.Class.Id == classId)
          .Select(t => t.Subject).ToList();
        var subjectList = examConfigSubjects.Intersect(classSubjects).ToList();
        //var subjectList = classTestList.Where(x => x.ExamConfiguration.Term.Id == requestMessage.TermId).Select(s => s.ExamConfiguration.Subject).Distinct().ToList();
        var examTypeList = classTestList.Where(x => x.ExamConfiguration.Term.Id == requestMessage.TermId).Select(s => s.ExamType).Distinct().ToList();

        responseMessage = new DisplayAddMarkPageResponseMessage(new List<DisplayAddMarkPageResponseMessage.Student>(),
          new List<DisplayAddMarkPageResponseMessage.Student>(),
          new List<DisplayAddMarkPageResponseMessage.Term>(), subjectList.Select(ToRequestSubject).ToList(),
          examTypeList.Select(ToRequestExamType).ToList(), new List<DisplayAddMarkPageResponseMessage.ClassTest>());
      }
      if (requestMessage.SessionId != null && requestMessage.TermId != null && requestMessage.SubjectId != null && requestMessage.ExamType == null)
      {
        var subjectIncludes = new[]
        {
          nameof(ClassTest.ExamConfiguration),
          nameof(ClassTest.ExamConfiguration)+"."+nameof(ClassTest.ExamConfiguration.Term),
          nameof(ClassTest.ExamConfiguration)+"."+nameof(ClassTest.ExamConfiguration.Subject)
        };
        var classTestList = _repository.Table<ClassTest>(subjectIncludes);
        var examTypeList = classTestList.Where(x => x.ExamConfiguration.Term.Id == requestMessage.TermId).Select(s => s.ExamType).Distinct().ToList();

        responseMessage = new DisplayAddMarkPageResponseMessage(new List<DisplayAddMarkPageResponseMessage.Student>(),
          new List<DisplayAddMarkPageResponseMessage.Student>(),
          new List<DisplayAddMarkPageResponseMessage.Term>(), new List<DisplayAddMarkPageResponseMessage.Subject>(),
          examTypeList.Select(ToRequestExamType).ToList(), new List<DisplayAddMarkPageResponseMessage.ClassTest>());
      }
      if (requestMessage.SessionId != null && requestMessage.TermId != null && requestMessage.SubjectId!= null && requestMessage.ExamType!=null)
      {
        var ctIncludes = new[]
        {
          nameof(ClassTest.ExamConfiguration),
          nameof(ClassTest.ExamConfiguration)+"."+nameof(ClassTest.ExamConfiguration.Subject)
        };
        var classTestList = _repository.Table<ClassTest>(ctIncludes)
          .Where(x => x.ExamConfiguration.Term.Id == requestMessage.TermId && x.ExamType == requestMessage.ExamType.Value && x.ExamConfiguration.Subject.Id == requestMessage.SubjectId.Value).ToList();


        responseMessage = new DisplayAddMarkPageResponseMessage(new List<DisplayAddMarkPageResponseMessage.Student>(),
          new List<DisplayAddMarkPageResponseMessage.Student>(),
          new List<DisplayAddMarkPageResponseMessage.Term>(), new List<DisplayAddMarkPageResponseMessage.Subject>(),
          new List<DisplayAddMarkPageResponseMessage.ExamType>(), classTestList.Select(ToRequestClassTest).ToList());
      }

      return responseMessage; //new DisplayAddMarkPageResponseMessage(studentSections.Select(ToRequestObject).ToList());
    }

    private DisplayAddMarkPageResponseMessage.ClassTest ToRequestClassTest(ClassTest classTest)
    {
      return new DisplayAddMarkPageResponseMessage.ClassTest
      {
        ClassTestId = classTest.Id, 
        ClassTestName = classTest.TestName
      };
    }

    private DisplayAddMarkPageResponseMessage.ExamType ToRequestExamType(ExamTypeEnum examType)
    {
      return new DisplayAddMarkPageResponseMessage.ExamType
      {
        ExamTypeId = (long)examType,
        ExamTypeName = examType.ToString()
      };
    }

    private DisplayAddMarkPageResponseMessage.Subject ToRequestSubject(Subject subject)
    {
      return new DisplayAddMarkPageResponseMessage.Subject
      {
        SubjectId = subject.Id,
        SubjectName = subject.SubjectName
      };
    }

    private DisplayAddMarkPageResponseMessage.Term ToRequestTerm(ExamTerm term)
    {
      return new DisplayAddMarkPageResponseMessage.Term
      {
        TermId = term.Id,
        TermName = term.TermName
      };
    }

    private Section GetSection(DisplayAddMarkPageRequestMessage requestMessage)
    {
      return _repository.GetById<ClassTest, Section>(requestMessage.TestId, x => x.Section).Section;
    }

    private static DisplayAddMarkPageResponseMessage.Student ToRequestObject(StudentSection studentSection,decimal fullMarks)
    {
      return new DisplayAddMarkPageResponseMessage.Student
      {
        StudentSectionId = studentSection.Id,
        RollNo = studentSection.RollNo,
        StudentName = studentSection.Student.FullName,
        ClassName = studentSection.Section.Class.ClassName,
        ShiftName = studentSection.Section.BranchMedium.Shift.ShiftName,
        SectionName = studentSection.Section.SectionName,
        FullMark = (int)fullMarks,
      };
    }
  }
}