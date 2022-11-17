using System.Threading.Tasks;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.UseCases.Authorization
{
  public interface IIsAuthorizedUseCase
  {
    Task<bool> IsAuthorized(string username, FeatureType.FeatureTypeEnum featureType, int featureCode);
  }
}