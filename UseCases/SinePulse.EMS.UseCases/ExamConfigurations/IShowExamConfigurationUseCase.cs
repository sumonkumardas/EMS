using SinePulse.EMS.Messages.ExamConfigurationMessages;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public interface IShowExamConfigurationUseCase
  {
    ShowExamConfigurationResponseMessage ShowExamConfiguration(ShowExamConfigurationRequestMessage message);
  }
}