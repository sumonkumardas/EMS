using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class ShowTermTestListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<TermTest> TermTestList { get; set; }

    public ShowTermTestListResponseMessage(ValidationResult validationResult, List<TermTest> classTestList)
    {
      ValidationResult = validationResult;
      TermTestList = classTestList;
    }

    public class TermTest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClassCode { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string ExamType { get; set; }
    }
  }
}