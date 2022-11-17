using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NakedObjects;

namespace SinePulse.EMS.NO.Domain.UserManagement
{
  [Table("AspNetUserRoles")]
  [DisplayName("UserRoles")]
  public class NOUserRoles
  {
    #region Primitive Properties

    [Key, Column(Order = 0)]
    [NakedObjectsIgnore]
    [ForeignKey("LoginUser")]
    public virtual string UserId { get; set; }

    [Key, Column(Order = 1)]
    [NakedObjectsIgnore]
    [ForeignKey("Role")]
    public virtual string RoleId { get; set; }

    #endregion

    #region Navigation Properties

    [MemberOrder(40), Disabled]
    public virtual NOLoginUser LoginUser { get; set; }

    [MemberOrder(50), Disabled]
    public virtual NORole Role { get; set; }

    #endregion
  }
}