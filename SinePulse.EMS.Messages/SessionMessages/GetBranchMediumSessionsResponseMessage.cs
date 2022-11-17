using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class GetBranchMediumSessionsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<SessionMessageModel> Sessions { get; }

    public GetBranchMediumSessionsResponseMessage(ValidationResult validationResult,
      IEnumerable<SessionMessageModel> sessions)
    {
      ValidationResult = validationResult;
      Sessions = sessions;
    }
  }
}