using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidEmployeeRegistrationChecker
  {
    bool IsValidRegistration(RegisterDataCheckRequestMessage registerData);
  }
}