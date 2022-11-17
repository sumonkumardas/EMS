using System;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class ShowStudentResponseMessage
  {
    public Student StudentData { get; }

    public ShowStudentResponseMessage(Student student)
    {
      StudentData = student;
    }

    public class Student
    {
      public long StudentId { get; set; }
      public string FullName { get; set; }
      public DateTime BirthDate { get; set; }
      public string Email { get; set; }
      public string MobileNo { get; set; }
      public StatusEnum Status { get; set; }
      public string ImageUrl { get; set; }
      public RelationTypeEnum Guardian { get; set; }
      public long InstituteId { get; set; }
      public string InstituteName { get; set; }
      public long BranchId { get; set; }
      public string BranchName { get; set; }
      public long MediumId { get; set; }
      public string MediumName { get; set; }
      public GroupEnum Group { get; set; }
      public long ClassId { get; set; }
      public string ClassName { get; set; }
      public long SectionId { get; set; }
      public string SectionName { get; set; }
      public int Roll { get; set; }
      public long BranchMediumId { get; set; }
      public string ShiftName { get; set; }
    }
  }
}