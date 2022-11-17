using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IAddEmployeeImageUseCase
  {
    string UploadEmployeeImage(AddEmployeeImageRequestMessage request);
  }
}