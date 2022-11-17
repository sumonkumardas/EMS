using System.Collections.Generic;
using System.Linq;
using NakedObjects.Services;
using SinePulse.EMS.NO.Domain.Features;
using SinePulse.EMS.NO.Domain.UserManagement;

namespace SinePulse.EMS.NO.Domain.Repositories
{
  public class LoggedInUserInfoRepository : AbstractFactoryAndRepository
  {
    IList<NOFeature> _features = new List<NOFeature>();

    public IList<NOFeature> GetFeatureListByLoginUser()
    {
      if (_features.Count > 0)
        return _features;

      NOLoginUser user = (from f in Container.Instances<NOLoginUser>()
        where f.Email == Container.Principal.Identity.Name
        select f).FirstOrDefault();

      if (user != null)
        _features = user.Role.Features;

      return _features;
    }
  }
}