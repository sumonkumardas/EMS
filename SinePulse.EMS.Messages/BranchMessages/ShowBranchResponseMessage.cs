using FluentValidation.Results;
using SinePulse.EMS.Domain.Academic;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.BranchMessages
{
  public class ShowBranchResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public Branch Branch { get; }
    public ICollection<Session> Sessions { get; }

    public ShowBranchResponseMessage(ValidationResult validationResult, Branch branch, ICollection<Session> sessions)
    {
      ValidationResult = validationResult;
      Branch = branch;
      Sessions = sessions;
    }
  }
}