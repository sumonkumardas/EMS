using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Features
{
  public class AddFeatureInRoleUseCase : IAddFeatureInRoleUseCase
  {
    private readonly IRepository _repository;
    private readonly RoleManager<Role> _roleManager;
    private readonly EmsDbContext _dbContext;

    public AddFeatureInRoleUseCase(IRepository repository, RoleManager<Role> roleManager, EmsDbContext dbContext)
    {
      _repository = repository;
      _roleManager = roleManager;
      _dbContext = dbContext;
    }

    public void AddFeatureInRole(AddFeatureInRoleRequestMessage message)
    {
      var role = _roleManager.FindByIdAsync(message.RoleId).GetAwaiter().GetResult();
      if (message.FeatureIds != null)
      {
        foreach (var featureId in message.FeatureIds)
        {
          var feature = _repository.GetById<Feature>(featureId);
          var roleFeature = new RoleFeature
          {
            Role = role,
            Feature = feature
          };
          _repository.Insert(roleFeature);
        }

        _dbContext.SaveChanges();
      }
    }
  }
}