using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.UserManagement;

namespace SinePulse.EMS.Domain.Features
{
  public class RoleFeature : BaseEntity
  {
    #region Navigation Properties

    public virtual Feature Feature { get; set; }

    public virtual Role Role { get; set; }

    #endregion
  }
}