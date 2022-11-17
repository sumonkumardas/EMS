using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.Site.Authorization
{
  internal class FeatureAuthorizationPolicyProvider : IAuthorizationPolicyProvider
  {
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public FeatureAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    {
      FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
      try
      {
        var tokens = policyName.Split(FeatureAuthorizeAttribute.Separator);
        var featureType = Enum.Parse<FeatureType.FeatureTypeEnum>(tokens[0]);
        var featureCode = int.Parse(tokens[1]);
        var policy = new AuthorizationPolicyBuilder();
        policy.AddRequirements(new FeatureAuthorizationRequirement(featureType, featureCode));
        return Task.FromResult(policy.Build());
      }
      catch (Exception)
      {
        return FallbackPolicyProvider.GetPolicyAsync(policyName);
      }
    }
  }
}