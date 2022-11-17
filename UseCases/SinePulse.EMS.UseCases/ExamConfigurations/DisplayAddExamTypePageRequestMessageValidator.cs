using FluentValidation;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class DisplayAddExamTypePageRequestMessageValidator : AbstractValidator<DisplayAddExamConfigurationPageRequestMessage>
  {
    public DisplayAddExamTypePageRequestMessageValidator()
    {
    }
    
  }
}