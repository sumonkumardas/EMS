using FluentValidation.Results;

namespace SinePulse.EMS.Messages.InstituteMessages
{
  public class AddInstituteResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long Id { get; set; }
    public AddInstituteResponseMessage(ValidationResult validationResult,long id)
    {
      ValidationResult = validationResult;
      Id = id;
    }
  }
}