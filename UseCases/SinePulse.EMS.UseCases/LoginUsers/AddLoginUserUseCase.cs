using System;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.LoginUserMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public class AddLoginUserUseCase : IAddLoginUserUseCase
  {
    private readonly IRepository _repository;
    private readonly UserManager<LoginUser> _userManager;
    private readonly EmsDbContext _dbContext;
    private readonly RoleManager<Role> _roleManager;

    public AddLoginUserUseCase(IRepository repository, UserManager<LoginUser> userManager, EmsDbContext dbContext, 
      RoleManager<Role> roleManager)
    {
      _repository = repository;
      _userManager = userManager;
      _dbContext = dbContext;
      _roleManager = roleManager;
    }

    public void AddLoginUser(AddLoginUserRequestMessage message)
    {
      var employee = _repository.GetById<Domain.Employees.Employee>(message.EmployeeId);
      if (!string.IsNullOrEmpty(message.BranchMediumId))
      {
        var branchMedium = _repository.GetById<BranchMedium>(Convert.ToInt64(message.BranchMediumId));
        employee.BranchMedium = branchMedium;
      }
      if (!string.IsNullOrEmpty(message.BranchId))
      {
        var branch = _repository.GetById<Branch>(Convert.ToInt64(message.BranchId));
        employee.Branch = branch;
      }
      if (!string.IsNullOrEmpty(message.InstituteId))
      {
        var institute = _repository.GetById<Institute>(Convert.ToInt64(message.InstituteId));
        employee.Institute = institute;
      }
      var role = _roleManager.FindByIdAsync(message.RoleId).GetAwaiter().GetResult();
      var loginUser = new LoginUser
      {
        UserName = employee.EmployeeCode,
        NormalizedUserName = employee.EmployeeCode,
        Email = employee.EmployeeCode
      };

      _userManager.CreateAsync(loginUser, message.Password).GetAwaiter().GetResult();
      _userManager.AddToRoleAsync(loginUser, role.Name).GetAwaiter().GetResult();

      employee.LoginUser = loginUser;
      employee.AuditFields.LastUpdatedBy = message.CurrentUserRoleName;
      employee.AuditFields.LastUpdatedDateTime = DateTime.Now;
      _dbContext.SaveChanges();
    }
  }
}