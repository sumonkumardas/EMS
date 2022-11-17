using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.RegisterMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Register
{
  public class RegisterUseCase : IRegisterUseCase
  {
    private readonly IRepository _repository;
    private readonly UserManager<LoginUser> _userManager;

    public RegisterUseCase(IRepository repository, UserManager<LoginUser> userManager)
    {
      _repository = repository;
      _userManager = userManager;
    }

    public RegisterResponseMessage Register(RegisterRequestMessage requestMessage)
    {
      var employee = _repository.Filter<Domain.Employees.Employee>(x => x.FullName == requestMessage.FullName)
        .AsEnumerable().First();
      var loginUser = new LoginUser
      {
        UserName = requestMessage.UserName,
        NormalizedUserName = requestMessage.UserName,
        Email = employee.EmailAddress,
        NormalizedEmail = employee.EmailAddress,
        EmailConfirmed = true,
        LockoutEnabled = false,
        SecurityStamp = Guid.NewGuid().ToString()
      };
      var identityResult = _userManager.CreateAsync(loginUser, requestMessage.Password).GetAwaiter().GetResult();
      var registerSucceeded = false;
      if (identityResult.Succeeded)
      {
        employee.LoginUser = loginUser;
        _repository.Insert(employee);
        registerSucceeded = true;
      }

      return new RegisterResponseMessage(registerSucceeded);
    }
  }
}