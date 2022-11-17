using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class FilterTermTestAddMarkFieldsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public TermTestAddMarkObjectsCollection TermTestsAddMarkObjects { get;}

    public FilterTermTestAddMarkFieldsResponseMessage(ValidationResult validationResult, TermTestAddMarkObjectsCollection termTestsAddMarkObjects)
    {
      ValidationResult = validationResult;
      TermTestsAddMarkObjects = termTestsAddMarkObjects;
    }

    public class TermTestAddMarkObjectsCollection
    {
      public IEnumerable<Class> Classes { get; set; }
      public IEnumerable<Subject> Subjects { get; set; }
      public IEnumerable<Group> Groups { get; set; }
      public IEnumerable<Section> Sections { get; set; }
      public IEnumerable<ExamType> ExamTypes { get; set; }
    }
    
    public class Class
    {
      public long ClassId { get; set; }
      public string ClassName { get; set; }
    }

    public class Subject
    {
      public long SubjectId { get; set; }
      public string SubjectName { get; set; }
    }

    public class Group
    {
      public long GroupId { get; set; }
      public string GroupName { get; set; }
    }
    
    public class ExamType
    {
      public long ExamTypeId { get; set; }
      public string ExamTypeName { get; set; }
    }

    public class Section
    {
      public long SectionId { get; set; }
      public string SectionName { get; set; }
    }
  }
}