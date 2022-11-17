using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NakedObjects;

namespace SinePulse.EMS.NO.Domain.UserManagement
{
  [Table("AspNetUserLogins")]
  [DisplayName("User Login")]
  public class NOUserLogins
  {
    #region Primitive Properties

    [Key, Column(Order = 0), NakedObjectsIgnore]
    public virtual string LoginProvider { get; set; }

    [Key, Column(Order = 1)]
    public virtual string ProviderKey { get; set; }

    [Key, Column(Order = 2)]
    [NakedObjectsIgnore]
    [ForeignKey("LoginUser")]
    public virtual string UserId { get; set; }

    #endregion

    #region Navigation Properties

    [MemberOrder(40), Disabled]
    public virtual NOLoginUser LoginUser { get; set; }

    #endregion
  }
}