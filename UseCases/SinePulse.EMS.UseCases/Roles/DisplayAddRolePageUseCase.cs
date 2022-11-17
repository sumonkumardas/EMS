using System;
using System.Linq;
using System.Linq.Expressions;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Roles
{
  public class DisplayAddRolePageUseCase : IDisplayAddRolePageUseCase
  {
    private readonly IRepository _repository;

    public DisplayAddRolePageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayAddRolePageResponseMessage DisplayAddRolePage(
      DisplayAddRolePageRequestMessage requestMessage)
    {
      return new DisplayAddRolePageResponseMessage();
    }
  }
}