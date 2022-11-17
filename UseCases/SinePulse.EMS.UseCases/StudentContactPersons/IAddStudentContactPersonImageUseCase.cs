
using SinePulse.EMS.Messages.StudentContactPersonMessages;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public interface IAddStudentContactPersonImageUseCase
  {
    string UploadStudentContactPersonImage(AddStudentContactPersonImageRequestMessage request);
  }
}