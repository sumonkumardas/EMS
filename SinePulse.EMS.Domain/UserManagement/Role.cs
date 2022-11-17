using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Domain.UserManagement
{
  public class Role : IdentityRole<string>
  {
    #region Primitive Properties
    public virtual RoleTypeEnum RoleType { get; set; }
    #endregion

    #region  Navigation Properties
    private ICollection<RoleFeature> _features = new List<RoleFeature>();
    public virtual ICollection<RoleFeature> Features
    {
      get { return _features; }
      set { _features = value; }
    }
    #endregion
  }
}