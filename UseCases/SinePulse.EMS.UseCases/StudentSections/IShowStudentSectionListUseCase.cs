using System.Collections.Generic;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public interface IShowStudentSectionListUseCase
  {
    List<ShowStudentSectionListResponseMessage.Student> ShowStudentSectionList(ShowStudentSectionListRequestMessage message);
  }
}