using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ExamConfigurationMessages;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public interface IAddExamConfigurationUseCase
  {
    ExamConfiguration AddExamConfiguration(AddExamConfigurationRequestMessage requestMessage);
  }
}