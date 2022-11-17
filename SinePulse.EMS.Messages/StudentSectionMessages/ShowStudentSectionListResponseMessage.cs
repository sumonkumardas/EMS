using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentSectionMessages
{
  public class ShowStudentSectionListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<Student> Students { get;}

    public ShowStudentSectionListResponseMessage(ValidationResult validationResult, List<Student> students)
    {
      ValidationResult = validationResult;
      Students = students;
    }

    public class Student
    {
      public long StudentId { get; set; }
      public string FullName { get; set; }
      public DateTime DOB { get; set; }
      public string MobileNo { get; set; }
      public int RollNo { get; set; }
    }
  }
}