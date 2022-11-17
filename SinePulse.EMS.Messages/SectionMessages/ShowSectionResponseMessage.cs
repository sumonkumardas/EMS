using FluentValidation.Results;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class ShowSectionResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public Section Section { get; }
    public BreakTime BreakTime { get; }

    public ShowSectionResponseMessage(ValidationResult validationResult, Section section,BreakTime breakTime)
    {
      ValidationResult = validationResult;
      Section = section;
      BreakTime = breakTime;
    }
  }
}