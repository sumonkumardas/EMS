using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public interface IAddStudentSectionUseCase
  {
    void AddStudentSection(AddStudentSectionRequestMessage message);
  }
}