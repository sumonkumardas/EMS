using Microsoft.AspNetCore.Authorization;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.Site.Authorization
{
  internal class FeatureAuthorizationRequirement : IAuthorizationRequirement
  {
    public FeatureType.FeatureTypeEnum FeatureType { get; }
    public int FeatureCode { get; }

    public FeatureAuthorizationRequirement(FeatureType.FeatureTypeEnum featureType, int featureCode)
    {
      FeatureType = featureType;
      FeatureCode = featureCode;
    }
  }
}