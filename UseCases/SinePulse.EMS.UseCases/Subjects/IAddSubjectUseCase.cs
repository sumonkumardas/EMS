using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public interface IAddSubjectUseCase
  {
    SubjectMessageModel AddSubject(AddSubjectRequestMessage requestMessage);
  }
}