using SinePulse.EMS.Messages.ExamConfigurationMessages;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public interface IShowExamTypeUseCase
  {
    ShowExamTypeResponseMessage ShowExamType(ShowExamTypeRequestMessage message);
  }
}