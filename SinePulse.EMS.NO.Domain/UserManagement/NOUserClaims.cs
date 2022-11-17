using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NakedObjects;

namespace SinePulse.EMS.NO.Domain.UserManagement
{
  [Table("AspNetUserClaims")]
  [DisplayName("User Claims")]
  public class NOUserClaims
  {
    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual int Id { get; set; }

    [NakedObjectsIgnore]
    [ForeignKey("LoginUser"), Required]
    public virtual string UserId { get; set; }

    [Optionally]
    public virtual string ClaimType { get; set; }

    [Optionally]
    public virtual string ClaimValue { get; set; }

    #endregion

    #region Navigation Properties

    [MemberOrder(40), Disabled]
    public virtual NOLoginUser LoginUser { get; set; }

    #endregion
  }
}