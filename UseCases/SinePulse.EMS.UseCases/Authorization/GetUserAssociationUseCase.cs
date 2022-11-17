using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Authorization
{
  public class GetUserAssociationUseCase : IGetUserAssociationUseCase
  {
    private readonly IRepository _repository;
    private readonly UserManager<LoginUser> _userManager;

    public GetUserAssociationUseCase(IRepository repository, UserManager<LoginUser> userManager)
    {
      _repository = repository;
      _userManager = userManager;
    }

    public GetUserAssociationResponseMessage GetUserAssociation(GetUserAssociationRequestMessage request)
    {
      decimal pendingAmount = 0;
      bool isBillDueDateOver = false;

      var loginUser = _userManager.FindByNameAsync(request.Username).GetAwaiter().GetResult();
      IList<RoleFeature> roleFeatures = GetFeaturesByRole(loginUser);
      var employee = _repository.Filter<Domain.Employees.Employee, Institute, Branch, BranchMedium>(
        x => x.LoginUser.Id == loginUser.Id,
        x => x.Institute,
        x => x.Branch,
        x => x.BranchMedium).First();

      if (employee.AssociatedWith == AssociationType.BranchMedium)
      {
        pendingAmount = GetPendingAmount(employee.BranchMedium.Id);
        isBillDueDateOver = IsBillDueDateOver(employee.BranchMedium.Id, pendingAmount);
      }

      return new GetUserAssociationResponseMessage(request.Username, employee.ImageUrl,
        employee.AssociatedWith, employee.Institute?.Id, employee.Institute?.Banner,
        employee.Branch?.Id, employee.BranchMedium?.Id, roleFeatures, pendingAmount, isBillDueDateOver);
    }

    private decimal GetPendingAmount(long branchMediumId)
    {
      decimal pendingAmount = 0;
      
      //NEED TO IMPLEMENT

      return pendingAmount;
    }
    private bool IsBillDueDateOver(long branchMediumId, decimal pendingAmount)
    {
      bool isBillDueDateOver = false;
      ServiceCharge serviceCharge = _repository.Filter<ServiceCharge, BranchMedium>(
          x => x.BranchMedium.Id == branchMediumId && x.EndDate == null,
          x => x.BranchMedium
          ).FirstOrDefault();
      if (serviceCharge != null)
      {
        DateTime billDueDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        billDueDate = billDueDate.AddDays(serviceCharge.PaymentBufferPeriod);
        if (DateTime.Today > billDueDate && pendingAmount > 0)
        {
          isBillDueDateOver = true;
        }
      }
      return isBillDueDateOver;
    }
    private IList<RoleFeature> GetFeaturesByRole(LoginUser loginUser)
    {
      List<RoleFeature> roleFeatures = new List<RoleFeature>();
      IList<string> roles = _userManager.GetRolesAsync(loginUser).GetAwaiter().GetResult();
      foreach(string role in roles)
      {
        var roleFeature = _repository.Filter<RoleFeature, Role, Feature>(
          x => x.Role.Name == role,
          x => x.Role,
          x => x.Feature).ToList();
        roleFeatures.AddRange(roleFeature);
      }
      return roleFeatures;
    }
    private static Messages.Shared.AssociationType ToMessageModelAssociationType(
      AssociationType associationType)
    {
      switch (associationType)
      {
        case AssociationType.Global:
          return Messages.Shared.AssociationType.Global;
        case AssociationType.Institute:
          return Messages.Shared.AssociationType.Institute;
        case AssociationType.Branch:
          return Messages.Shared.AssociationType.Branch;
        case AssociationType.BranchMedium:
          return Messages.Shared.AssociationType.BranchMedium;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}