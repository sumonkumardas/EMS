using System.Linq;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidEmployeeRegistrationChecker : IValidEmployeeRegistrationChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidEmployeeRegistrationChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidRegistration(RegisterDataCheckRequestMessage registerData)
    {
      //var employees = _dbContext.Employees.Where(x => x.FullName == registerData.FullName &&
      //                                                registerData.DOB == registerData.DOB &&
      //                                                registerData.JoiningDate == registerData.JoiningDate);
      //if (employees.Any())
      //{
      //  var employee = employees.First();

      //  return !employee.IsAccountRegistered;
      //}

      return true;
    }
  }
}