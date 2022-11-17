using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SinePulse.EMS.UseCases.Authorization;

namespace SinePulse.EMS.Site.Authorization
{
  internal class FeatureAuthorizationHandler : AuthorizationHandler<FeatureAuthorizationRequirement>
  {
    private readonly ISingletonIsAuthorizedUseCase _isAuthorizedUseCase;

    public FeatureAuthorizationHandler(ISingletonIsAuthorizedUseCase isAuthorizedUseCase)
    {
      _isAuthorizedUseCase = isAuthorizedUseCase;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
      FeatureAuthorizationRequirement requirement)
    {
      if (await _isAuthorizedUseCase.IsAuthorized(context.User.Identity.Name, requirement.FeatureType,
        requirement.FeatureCode))
      {
        context.Succeed(requirement);
      }
      else
      {
        var redirectContext = (AuthorizationFilterContext) context.Resource;
        redirectContext.Result = new RedirectToActionResult("Login", "Account", null);
        context.Succeed(requirement);
      }
    }
  }
}