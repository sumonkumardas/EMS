
using SinePulse.EMS.Messages.JobTypeMessages;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public interface IShowJobTypeListUseCase
  {
    List<JobTypeMessageModel> ShowJobTypeList(ShowJobTypeListRequestMessage request);
  }
}