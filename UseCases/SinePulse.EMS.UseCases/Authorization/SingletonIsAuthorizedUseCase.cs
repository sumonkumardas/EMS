using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.UseCases.Authorization
{
  public class SingletonIsAuthorizedUseCase : ISingletonIsAuthorizedUseCase
  {
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SingletonIsAuthorizedUseCase(IServiceScopeFactory serviceScopeFactory)
    {
      _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<bool> IsAuthorized(string username, FeatureType.FeatureTypeEnum featureType, int featureCode)
    {
      using (var serviceScope = _serviceScopeFactory.CreateScope())
      {
        var scopedService = serviceScope.ServiceProvider.GetService<IIsAuthorizedUseCase>();
        return await scopedService.IsAuthorized(username, featureType, featureCode);
      }
    }
  }
}