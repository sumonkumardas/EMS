using System.Collections.Generic;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.Classes
{
  public interface IGetSubjectsToAddInClassUseCase
  {
    IEnumerable<SubjectMessageModel> GetSubjects(GetSubjectsToAddInClassRequestMessage message);
  }
}                        