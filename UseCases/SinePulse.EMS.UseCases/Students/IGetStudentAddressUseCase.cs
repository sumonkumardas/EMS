using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IGetStudentAddressUseCase
  {
    GetStudentAddressResponseMessage.StudentAddress GetStudentAddress(GetStudentAddressRequestMessage message);
  }
}