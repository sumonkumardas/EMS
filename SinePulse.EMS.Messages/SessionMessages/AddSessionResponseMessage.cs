using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class AddSessionResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long SessionId { get; }
    public ObjectTypeEnum ObjectType { get; }
    public long ObjectId { get; }

    public AddSessionResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public AddSessionResponseMessage(ValidationResult validationResult, long sessionId, ObjectTypeEnum objectType,
      long objectId)
    {
      ValidationResult = validationResult;
      SessionId = sessionId;
      ObjectType = objectType;
      ObjectId = objectId;
    }
  }
}