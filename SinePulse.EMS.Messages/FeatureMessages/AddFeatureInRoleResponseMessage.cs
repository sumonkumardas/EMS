using FluentValidation.Results;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class AddFeatureInRoleResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddFeatureInRoleResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}