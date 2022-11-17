using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.WaiverMessages;

namespace SinePulse.EMS.UseCases.Waivers
{
  public interface IGetStudentWaiversUseCase
  {
    IEnumerable<StudentWaiverMessageModel> GetStudentWaivers(GetStudentWaiversRequestMessage message);
  }
}