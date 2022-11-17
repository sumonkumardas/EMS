using FluentValidation.Results;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class AddDesignationResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long DesignationId { get; }
    public AddDesignationResponseMessage(ValidationResult validationResult, long designationId)
    {
      ValidationResult = validationResult;
      DesignationId = designationId;
    }
  }
}