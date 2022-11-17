using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class ViewSessionResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public SessionMessageModel Session { get; }

    public ViewSessionResponseMessage(ValidationResult validationResult, SessionMessageModel session)
    {
      ValidationResult = validationResult;
      Session = session;
    }
  }
}