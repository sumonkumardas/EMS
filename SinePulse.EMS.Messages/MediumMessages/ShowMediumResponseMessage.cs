using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class ShowMediumResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public MediumMessageModel Medium { get; }

    public ShowMediumResponseMessage(ValidationResult validationResult, MediumMessageModel medium)
    {
      ValidationResult = validationResult;
      Medium = medium;
    }
  }
}