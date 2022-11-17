using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public interface IEditSubjectUseCase
  {
    SubjectMessageModel UpdateSubject(EditSubjectRequestMessage message);
  }
}