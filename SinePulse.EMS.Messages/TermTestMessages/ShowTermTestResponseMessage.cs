using System;
using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.ExamConfigurationMessages;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class ShowTermTestResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public TermTest PickedTermTest { get; set; }

    public ShowTermTestResponseMessage(ValidationResult validationResult, TermTest termTest)
    {
      ValidationResult = validationResult;
      PickedTermTest = termTest;
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
        public ExamTypeEnum ExamType { get; set; }
        public long ClassId { get; set; }
        public long SubjectId { get; set; }
        public long TermId { get; set; }
        public long TermBranchId { get; set; }
        public long GroupId { get; set; }
    }
  }
}