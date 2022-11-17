using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class ShowSessionListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<SessionMessageModel> Sessions { get; set; }

    public ShowSessionListResponseMessage(ValidationResult validationResult, IEnumerable<SessionMessageModel> sessions)
    {
      ValidationResult = validationResult;
      Sessions = sessions;
    }
  }
}