using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ExamConfigurationMessages;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public interface IEditExamConfigurationUseCase
  {
    void EditExamConfiguration(EditExamConfigurationRequestMessage requestMessage);
  }
}