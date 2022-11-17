using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidEmployeeRegisterDataChecker : IValidEmployeeRegisterDataChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidEmployeeRegisterDataChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidEmployee(RegisterDataCheckRequestMessage registerData)
    {
      foreach (var employee in _dbContext.Employees)
      {
        if (registerData.FullName == employee.FullName &&
            registerData.DOB == employee.DOB &&
            registerData.JoiningDate == employee.JoiningDate)
        {
          return true;
        }
      }

      return false;
    }
  }
}