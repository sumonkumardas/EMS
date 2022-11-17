using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public interface IAddJobTypeUseCase
  {
    JobTypeMessageModel AddJobType(AddJobTypeRequestMessage requestMessage);
  }
}