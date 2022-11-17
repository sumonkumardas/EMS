using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public interface IShowJobTypeUseCase
  {
    JobTypeMessageModel ShowJobType(ShowJobTypeRequestMessage message);
  }
}