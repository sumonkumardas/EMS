using SinePulse.EMS.Messages.JobTypeMessages;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public interface IEditJobTypeUseCase
  {
    void EditJobType(EditJobTypeRequestMessage message);
  }
}