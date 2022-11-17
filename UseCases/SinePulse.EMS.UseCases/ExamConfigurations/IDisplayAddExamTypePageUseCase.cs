using SinePulse.EMS.Messages.ExamConfigurationMessages;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public interface IDisplayAddExamTypePageUseCase
  {
    DisplayAddExamConfigurationPageResponseMessage DisplayAddExamTypePage(DisplayAddExamConfigurationPageRequestMessage requestMessage);
  }
}