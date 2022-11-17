using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;
using System;

namespace SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages
{
  public class GetCommitteeMemberPersonalInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public CommitteeMemberPersonalInfo CommitteeMemberPersonalInfos { get; }

    public GetCommitteeMemberPersonalInfoResponseMessage(ValidationResult validationResult,
      CommitteeMemberPersonalInfo employeePersonalInfos)
    {
      ValidationResult = validationResult;
      CommitteeMemberPersonalInfos = employeePersonalInfos;
    }

    public class CommitteeMemberPersonalInfo
    {
      public string FathersName { get; set; }
      public string MothersName { get; set; }
      public string SpouseName { get; set; }
      public string Gender { get; set; }
      public GenderEnum GenderEnum { get; set; }
      public string Religion { get; set; }
      public ReligionEnum ReligionEnum { get; set; }
      public string BloodGroup { get; set; }
      public BloodGroupEnum BloodGroupEnum { get; set; }
    }
  }
}
