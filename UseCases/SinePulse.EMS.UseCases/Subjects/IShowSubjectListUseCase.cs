using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public interface IShowSubjectListUseCase
  {
    IEnumerable<SubjectMessageModel> ShowSubjectList(ShowSubjectListRequestMessage message);
  }
}