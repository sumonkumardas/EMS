using FluentValidation.Results;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class RemoveFeatureFromRoleResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public RemoveFeatureFromRoleResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}