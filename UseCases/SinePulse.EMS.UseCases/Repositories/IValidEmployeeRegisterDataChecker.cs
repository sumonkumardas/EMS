using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidEmployeeRegisterDataChecker
  {
    bool IsValidEmployee(RegisterDataCheckRequestMessage registerData);
  }
}