using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class DisplayBranchMediumResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public Institute DisplayModelInstitute { get; set; }
    public Branch DisplayModelBranch { get; set; }
    public BranchMedium DisplayBranchMedium { get; set; }
    public List<Session> Sessions { get; set; }

    public DisplayBranchMediumResponseMessage(ValidationResult validationResult, Institute displayModelInstitute, Branch displayModelBranch, BranchMedium displayBranchMedium, List<Session> sessions)
    {
      ValidationResult = validationResult;
      DisplayModelInstitute = displayModelInstitute;
      DisplayModelBranch = displayModelBranch;
      DisplayBranchMedium = displayBranchMedium;
      Sessions = sessions;
    }

    public class Institute
    {
      public long InstituteId { get; set; }
      public string InstituteName { get; set; }
    }

    public class Branch
    {
      public long BranchId { get; set; }
      public string BranchName { get; set; }
    }

    public class BranchMedium
    {
      public long BranchMediumId { get; set; }
      public string BranchMediumName { get; set; }
      public string ShiftName { get; set; }
    }

    public class Session
    {
      public long SessionId { get; set; }
      public string SessionName { get; set; }
    }
  }
}