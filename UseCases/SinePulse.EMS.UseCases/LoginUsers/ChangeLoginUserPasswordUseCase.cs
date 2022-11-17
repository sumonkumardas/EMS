using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.LoginUserMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public class ChangeLoginUserPasswordUseCase : IChangeLoginUserPasswordUseCase
  {
    private readonly UserManager<LoginUser> _userManager;
    private readonly EmsDbContext _dbContext;

    public ChangeLoginUserPasswordUseCase(UserManager<LoginUser> userManager, EmsDbContext dbContext)
    {
      _userManager = userManager;
      _dbContext = dbContext;
    }

    public void ChangeLoginUserPassword(ChangeLoginUserPasswordRequestMessage message)
    {
      var user = _userManager.FindByNameAsync(message.EmployeeCode).Result;
      var result = _userManager.ChangePasswordAsync(user, message.OldPassword, message.NewPassword).Result;
      _dbContext.SaveChanges();
    }
  }
}