using System.Collections.Generic;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public interface IGetOptionalSubjectListUseCase
  {
    IEnumerable<GetOptionalSubjectListResponseMessage.Subject> GetOptionalSubjectList(
      GetOptionalSubjectListRequestMessage message);
  }
}