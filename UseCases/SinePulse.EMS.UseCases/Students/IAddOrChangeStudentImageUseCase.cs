using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IAddOrChangeStudentImageUseCase
  {
    string UploadStudentImage(AddOrChangeStudentImageRequestMessage request);
  }
}