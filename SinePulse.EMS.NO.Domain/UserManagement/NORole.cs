using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NakedObjects;
using NakedObjects.Menu;
using SinePulse.EMS.NO.Domain.Features;
using SinePulse.EMS.NO.Domain.Repositories;

namespace SinePulse.EMS.NO.Domain.UserManagement
{
  [Table("AspNetRoles")]
  [DisplayName("Role")]
  [Bounded]
  public class NORole
  {
    private const string SuperAdmin = "SuperAdmin";

    #region Injected Services

    public IDomainObjectContainer Container { set; protected get; }
    public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }

    #endregion

    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual string Id { get; set; }

    [Title, Required]
    [MemberOrder(10)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string Name { get; set; }

    #endregion

    #region Get Properties    

    #region Users  

    [MemberOrder(70), NotMapped]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("Users")]
    [TableView(false, "Email")]
    public IList<NOLoginUser> Users
    {
      get
      {
        IList<NOLoginUser> users = (from r in Container.Instances<NOUserRoles>()
          where r.Role.Id == this.Id
          select r.LoginUser).OrderBy(o => o.UserName).ToList();
        return users;
      }
    }

    #endregion

    #region Features

    [MemberOrder(50), NotMapped]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [DisplayName("Features")]
    [TableView(false, "FeatureName", "FeatureType")]
    public IList<NOFeature> Features
    {
      get
      {
        IList<NOFeature> features = (from r in Container.Instances<NORoleFeatures>()
          where r.Role.Id == this.Id
          select r.Feature).OrderBy(o => o.FeatureName).ToList();
        return features;
      }
    }

    #endregion

    #endregion

    #region Behavior

    #region Assign Feature To Role

    public void AssignFeatureToRole(NOFeatureType type, IQueryable<NOFeature> features)
    {
      foreach (NOFeature f in features)
      {
        NORoleFeatures roleFeatures = Container.NewTransientInstance<NORoleFeatures>();

        roleFeatures.Role = this;
        roleFeatures.Feature = f;

        Container.Persist(ref roleFeatures);
      }
    }

    //[PageSize(10)]
    //public IQueryable<Feature> AutoComplete1AssignFeatureToRole (FeatureType type, [MinLength(3)] string name)
    //{
    //	return
    //		Container.Instances<Feature>()
    //			.Where(w => w.FeatureName.Contains(name) && w.FeatureType.FeatureTypeId == type.FeatureTypeId);
    //}
    public IList<NOFeature> Choices1AssignFeatureToRole(NOFeatureType type)
    {
      if (type == null) return new List<NOFeature>();
      IList<int> featureIds = (from f in this.Features
        where f.FeatureType.FeatureTypeId == type.FeatureTypeId
        select f.FeatureId).ToList();

      IList<NOFeature> features = (from f in Container.Instances<NOFeature>()
        where f.FeatureType.FeatureTypeId == type.FeatureTypeId
              && (!featureIds.Contains(f.FeatureId))
        select f).OrderBy(o => o.FeatureName).ToList();
      return features;
    }

    public bool HideAssignFeatureToRole()
    {
      //if (this.Name == SuperAdmin) return true;
      IList<NOFeature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

      NOFeature feature =
        features.Where(w => w.FeatureCode == (int) NOFeature.UserManagementEnum.AssignFeatureToRole
                            && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
          .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    #endregion

    #region Remove Feature From Role

    public void RemoveFeatureFromRole(NOFeatureType type, IEnumerable<NOFeature> features)
    {
      foreach (NOFeature f in features)
      {
        NORoleFeatures roleFeature = (from rf in Container.Instances<NORoleFeatures>()
          where rf.Role.Id == this.Id
                && rf.Feature.FeatureId == f.FeatureId
          select rf).Single();
        Container.DisposeInstance(roleFeature);
      }
    }

    public bool HideRemoveFeatureFromRole()
    {
      if (this.Name == SuperAdmin) return true;
      IList<NOFeature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

      NOFeature feature =
        features.Where(w => w.FeatureCode == (int) NOFeature.UserManagementEnum.RemoveFeatureFromRole
                            && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
          .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    public IList<NOFeature> Choices1RemoveFeatureFromRole(NOFeatureType type)
    {
      if (type == null) return new List<NOFeature>();
      IList<NOFeature> features = (from f in this.Features
        where f.FeatureType.FeatureTypeId == type.FeatureTypeId
        select f).OrderBy(o => o.FeatureName).ToList();
      return features;
    }

    #endregion

    #region Edit Role Enable Disable

    public string DisablePropertyDefault()
    {
      string msg = "You do not have permission to Edit";
      if (this.Name == SuperAdmin) return msg;
      IList<NOFeature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

      NOFeature feature =
        features.Where(w => w.FeatureCode == (int) NOFeature.UserManagementEnum.EditRole
                            && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
          .FirstOrDefault();

      if (feature == null)
      {
        return msg;
      }

      return null;
    }

    #endregion

    #endregion

    #region Menu

    public static void Menu(IMenu menu)
    {
      menu.AddAction("AssignFeatureToRole");
      menu.AddAction("RemoveFeatureFromRole");
    }

    #endregion
  }
}