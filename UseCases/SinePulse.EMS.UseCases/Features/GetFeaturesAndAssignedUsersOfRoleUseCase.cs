using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeaturesAndAssignedUsersOfRoleUseCase : IGetFeaturesAndAssignedUsersOfRoleUseCase
  {
    private readonly IRepository _repository;
    private readonly UserManager<LoginUser> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public GetFeaturesAndAssignedUsersOfRoleUseCase(IRepository repository, UserManager<LoginUser> userManager,
      RoleManager<Role> roleManager)
    {
      _repository = repository;
      _userManager = userManager;
      _roleManager = roleManager;
    }

    public GetFeaturesAndAssignedUsersOfRoleResponseMessage.FeaturesAndUsers GetFeaturesAndAssignedUsersOfRole(
      GetFeaturesAndAssignedUsersOfRoleRequestMessage message)
    {
      var features = _repository.Table<RoleFeature>()
        .Include(nameof(Feature))
        .Where(r => r.Role.Id == message.RoleId)
        .Select(r => r.Feature)
        .ToList();
      var requestData = new GetFeaturesAndAssignedUsersOfRoleResponseMessage.FeaturesAndUsers();
      requestData.Features = ToRequestFeatures(features);

      var identityRole = _roleManager.FindByIdAsync(message.RoleId).GetAwaiter().GetResult();
      var loginUsers = _userManager.GetUsersInRoleAsync(identityRole.Name).GetAwaiter().GetResult();
      var userList = new List<GetFeaturesAndAssignedUsersOfRoleResponseMessage.User>();
      foreach (var user in loginUsers)
      {
        var employee = _repository.Table<Domain.Employees.Employee>()
          .FirstOrDefault(e => e.EmployeeCode == user.UserName);
        if (employee != null)
          userList.Add(new GetFeaturesAndAssignedUsersOfRoleResponseMessage.User
          {
            EmployeeId = employee.Id,
            EmployeeName = employee.FullName
          });
      }

      requestData.Users = userList;
      return requestData;
    }

    private List<GetFeaturesAndAssignedUsersOfRoleResponseMessage.Feature> ToRequestFeatures(List<Feature> features)
    {
      var requestFeatures = new List<GetFeaturesAndAssignedUsersOfRoleResponseMessage.Feature>();
      if (features != null)
        foreach (var feature in features)
        {
          requestFeatures.Add(new GetFeaturesAndAssignedUsersOfRoleResponseMessage.Feature
          {
            FeatureId = feature.Id,
            FeatureCode = feature.FeatureCode,
            FeatureName = feature.FeatureName,
            FeatureType = feature.FeatureType
          });
        }

      return requestFeatures;
    }
  }
}