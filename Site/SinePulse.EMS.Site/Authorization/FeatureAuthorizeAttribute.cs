using System;
using Microsoft.AspNetCore.Authorization;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.Site.Authorization
{
  internal class FeatureAuthorizeAttribute : AuthorizeAttribute
  {
    public const string Separator = ":";

    public FeatureAuthorizeAttribute(FeatureType.FeatureTypeEnum featureType, int featureCode) =>
      Policy = $"{featureType}{Separator}{featureCode}";

    public int FeatureCode
    {
      get
      {
        var tokens = Policy.Split(Separator);
        return int.Parse(tokens[1]);
      }
      set
      {
        var tokens = Policy.Split(Separator);
        Policy = $"{tokens[0]}{Separator}{value}";
      }
    }

    public FeatureType.FeatureTypeEnum FeatureType
    {
      get
      {
        var tokens = Policy.Split(Separator);
        return Enum.Parse<FeatureType.FeatureTypeEnum>(tokens[0]);
      }
      set
      {
        var tokens = Policy.Split(Separator);
        Policy = $"{value}{Separator}{tokens[1]}";
      }
    }
  }
}