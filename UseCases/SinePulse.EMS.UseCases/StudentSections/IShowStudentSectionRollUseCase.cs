using System.Collections.Generic;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public interface IShowStudentSectionRollUseCase
  {
    int ShowStudentSectionRoll(ShowStudentSectionRollRequestMessage message);
  }
}