using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NakedObjects;
using SinePulse.EMS.NO.Domain.UserManagement;

namespace SinePulse.EMS.NO.Domain.Features
{
  [Table("RoleFeatures")]
  [DisplayName("RoleFeatures")]
  public class NORoleFeatures
  {
    #region Primitive Properties

    [Key, Column(Order = 0)]
    [NakedObjectsIgnore]
    [ForeignKey("Feature")]
    public virtual int FeatureId { get; set; }

    [Key, Column(Order = 1)]
    [NakedObjectsIgnore]
    [ForeignKey("Role")]
    public virtual string RoleId { get; set; }

    #endregion

    #region Navigation Properties

    [MemberOrder(40), Disabled]
    public virtual NOFeature Feature { get; set; }

    [MemberOrder(50), Disabled]
    public virtual NORole Role { get; set; }

    #endregion
  }
}