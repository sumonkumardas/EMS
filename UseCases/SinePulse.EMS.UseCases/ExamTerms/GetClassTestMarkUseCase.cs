using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.ExamTermMessages;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetClassTestMarkUseCase : IGetClassTestMarkUseCase
  {
    private readonly IRepository _repository;

    public GetClassTestMarkUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetClassTestMarksResponseMessage.ClassTest GetClassTestMarks(GetClassTestMarksRequestMessage requestMessage)
    {
      var term = _repository.GetById<ExamTerm>(requestMessage.TermId);
      var classTestMarks = _repository.Table<ClassTestMark>()
        .Include(nameof(ClassTest))
        .Include(nameof(StudentSection))
        .Include(nameof(StudentSection) + "." + nameof(StudentSection.Student))
        .Include(nameof(ClassTest) + "." + nameof(ClassTest.ExamConfiguration))
        .Include(nameof(ClassTest) + "." + nameof(ClassTest.ExamConfiguration) + "." + nameof(ClassTest.ExamConfiguration.Class))
        .Include(nameof(ClassTest) + "." + nameof(ClassTest.ExamConfiguration)+ "." + nameof(ClassTest.ExamConfiguration.Term))
        .Include(nameof(ClassTest) + "." + nameof(ClassTest.ExamConfiguration) + "." + nameof(ClassTest.ExamConfiguration.Subject))
        .Where(t => t.ClassTest.ExamConfiguration.Term.Id == requestMessage.TermId && t.StudentSection.Student.Id == requestMessage.StudentId)
        .ToList();
      GetClassTestMarksResponseMessage.ClassTest ctMark = ToRequestClassTestMarks(classTestMarks);
      return ctMark;
    }

    private GetClassTestMarksResponseMessage.ClassTest ToRequestClassTestMarks(List<ClassTestMark> classTestMarks)
    {
      var classTest = new GetClassTestMarksResponseMessage.ClassTest();
      classTest.ClassTestMarks = new List<GetClassTestMarksResponseMessage.ClassTestMark>();
      foreach (var classTestMark in classTestMarks)
      {
        classTest.Roll = classTestMark.StudentSection.RollNo;
        classTest.ClassName = classTestMark.ClassTest.ExamConfiguration.Class.ClassName;
        classTest.ClassTestMarks.Add(new GetClassTestMarksResponseMessage.ClassTestMark()
        {
          ExamType = classTestMark.ClassTest.ExamType.ToString(),
          SubjectName = classTestMark.ClassTest.ExamConfiguration.Subject.SubjectName,
          FullMarks = (int) classTestMark.ClassTest.FullMarks,
          ObtainedMarks = classTestMark.ObtainedMark

        });
      }

      return classTest;
    }
  }
}