using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NakedObjects;

namespace SinePulse.EMS.NO.Domain.Features
{
  [Table("FeatureTypes")]
  [DisplayName("Feature Type")]
  [Bounded]
  public class NOFeatureType
  {
    #region Injected Services

    public IDomainObjectContainer Container { set; protected get; }

    #endregion

    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual int FeatureTypeId { get; set; }

    [Title, Required]
    [MemberOrder(10)]
    [StringLength(50)]
    public virtual string FeatureTypeName { get; set; }

    [MemberOrder(10)]
    public virtual int FeatureTypeCode { get; set; }

    #endregion

    #region Get Properties

    #region Features

    [MemberOrder(50), NotMapped]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("Features")]
    [TableView(false, "FeatureName")]
    public IList<NOFeature> Features
    {
      get
      {
        IList<NOFeature> features = (from r in Container.Instances<NOFeature>()
          where r.FeatureType.FeatureTypeId == this.FeatureTypeId
          select r).ToList();
        return features;
      }
    }

    #endregion

    #endregion

    #region Feature Type Enums

    public enum FeatureTypeEnum
    {
      AcademicSetup = 1,
      EmployeeSetup = 2,
      GeneralSetup = 3,
      PayrollSetup = 4,
      FinanceSetup = 5,
      Session = 6,
      Institute = 7,
      Branch = 8,
      BranchMedium = 9,
      Section = 10,
      Examination = 11,
      Student = 12,
      Employee = 13,
      UserManagement = 14
    }

    #endregion

    #region Behavior

    #region Edit Feature Type Enable Disable

    public string DisablePropertyDefault()
    {
      return "You do not have permission to Edit";
    }

    #endregion

    #endregion
  }
}