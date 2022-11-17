using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IShowStudentListUseCase
  {
    IEnumerable<StudentMessageModel> ShowStudentsList(ShowStudentListRequestMessage message);
  }
}