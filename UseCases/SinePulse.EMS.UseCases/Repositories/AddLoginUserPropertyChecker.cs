using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class AddLoginUserPropertyChecker : IAddLoginUserPropertyChecker
  {
    private readonly IRepository _repository;
    private readonly UserManager<LoginUser> _userManager;

    public AddLoginUserPropertyChecker(IRepository repository, UserManager<LoginUser> userManager)
    {
      _repository = repository;
      _userManager = userManager;
    }

    public bool IsLoginUserAlreadyCreated(long employeeId)
    {
      var employee = _repository.GetById<Domain.Employees.Employee>(employeeId);
      var loginUser = _userManager.FindByNameAsync(employee.EmployeeCode).Result;
      return loginUser == null;
    }
  }
}