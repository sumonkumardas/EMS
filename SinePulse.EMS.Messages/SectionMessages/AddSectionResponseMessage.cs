using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class AddSectionResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long SectionId { get; set; }

    public AddSectionResponseMessage(ValidationResult validationResult,long sectionId)
    {
      ValidationResult = validationResult;
      SectionId = sectionId;
    }
  }
}