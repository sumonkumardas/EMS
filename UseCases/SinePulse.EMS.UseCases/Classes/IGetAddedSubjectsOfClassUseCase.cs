using System.Collections.Generic;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.Classes
{
  public interface IGetAddedSubjectsOfClassUseCase
  {
    IEnumerable<GetAddedSubjectsOfClassResponseMessage.SubjectWithClass> GetSubjects(GetAddedSubjectsOfClassRequestMessage message);
  }
}