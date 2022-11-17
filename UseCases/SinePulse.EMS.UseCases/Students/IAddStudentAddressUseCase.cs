using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IAddStudentAddressUseCase
  {
    void AddStudentAddress(AddStudentAddressRequestMessage message);
  }
}