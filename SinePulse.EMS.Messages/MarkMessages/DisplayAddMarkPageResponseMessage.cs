using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class DisplayAddMarkPageResponseMessage
  {
    public ICollection<Student> Students { get; }
    public ICollection<Student> OtherSectionStudents { get; }
    public ICollection<Term> Terms { get; }
    public ICollection<Subject> Subjects { get; }
    public ICollection<ExamType> ExamTypes { get; }
    public ICollection<ClassTest> ClassTests { get; }

    public DisplayAddMarkPageResponseMessage(ICollection<Student> students, ICollection<Student> otherSectionStudents,
      ICollection<Term> terms, ICollection<Subject> subjects, ICollection<ExamType> examTypes,
      ICollection<ClassTest> classTests)
    {
      Students = students;
      OtherSectionStudents = otherSectionStudents;
      Terms = terms;
      Subjects = subjects;
      ExamTypes = examTypes;
      ClassTests = classTests;
    }

    public class Student
    {
      public long StudentSectionId { get; set; }
      public int RollNo { get; set; }
      public string StudentName { get; set; }
      public string ClassName { get; set; }
      public string ShiftName { get; set; }
      public string SectionName { get; set; }
      public int FullMark { get; set; }
    }

    public class Term
    {
      public long TermId { get; set; }
      public string TermName { get; set; }
    }

    public class Subject
    {
      public long SubjectId { get; set; }
      public string SubjectName { get; set; }
    }

    public class ExamType
    {
      public long ExamTypeId { get; set; }
      public string ExamTypeName { get; set; }
    }

    public class ClassTest
    {
      public long ClassTestId { get; set; }
      public string ClassTestName { get; set; }
    }
  }
}