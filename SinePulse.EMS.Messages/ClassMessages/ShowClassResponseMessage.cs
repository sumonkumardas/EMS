using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class ShowClassResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ClassMessageModel Class { get; }

    public ShowClassResponseMessage(ValidationResult validationResult, ClassMessageModel @class)
    {
      ValidationResult = validationResult;
      Class = @class;
    }
  }
}