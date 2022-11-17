using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public interface IAddOptionalSubjectUseCase
  {
    void AddOptionalSubject(AddOptionalSubjectRequestMessage requestMessage);
  }
}