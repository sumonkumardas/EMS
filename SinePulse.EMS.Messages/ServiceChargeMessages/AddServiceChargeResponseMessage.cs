using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class AddServiceChargeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ServiceChargeId { get; }

    public AddServiceChargeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
    public AddServiceChargeResponseMessage(ValidationResult validationResult, long serviceChargeId)
    {
      ValidationResult = validationResult;
      ServiceChargeId = serviceChargeId;
    }
  }
}
