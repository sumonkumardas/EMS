using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Authorization
{
  public class IsAuthorizedUseCase : IIsAuthorizedUseCase
  {
    private readonly IRepository _repository;
    private readonly UserManager<LoginUser> _userManager;

    public IsAuthorizedUseCase(IRepository repository, UserManager<LoginUser> userManager)
    {
      _repository = repository;
      _userManager = userManager;
    }

    public async Task<bool> IsAuthorized(string username, FeatureType.FeatureTypeEnum featureType, int featureCode)
    {
      if (username == null) return false;

      var user = await _userManager.FindByNameAsync(username);

      if (user == null) return false;

      var roleNames = await _userManager.GetRolesAsync(user);

      return roleNames.Any(roleName => ContainsFeature(featureType, featureCode, roleName));
    }

    private bool ContainsFeature(FeatureType.FeatureTypeEnum featureType, int featureCode, string roleName)
    {
      var roleFeatures = _repository.Filter<RoleFeature, Feature, FeatureType>(
        x => x.Role.Name == roleName,
        x => x.Feature,
        x => x.Feature.FeatureType).AsEnumerable();

      return roleFeatures.Any(roleFeature => IsFeatureMatched(featureType, featureCode, roleFeature.Feature));
    }

    private static bool IsFeatureMatched(FeatureType.FeatureTypeEnum featureTypeEnum, int featureCode,
      Feature feature)
    {
      return feature.FeatureType.FeatureTypeName == featureTypeEnum.ToString() &&
             (featureCode == -1 || featureCode == feature.FeatureCode);
    }
  }
}