using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Services;
using SinePulse.EMS.NO.Domain.Features;
using SinePulse.EMS.NO.Domain.UserManagement;
using SinePulse.EMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Repositories
{
  [DisplayName("Profile Management")]
  public class UserManagementRepository : AbstractFactoryAndRepository
  {
    private const int UserCodeLength = 6;

    #region Injected Services
    public LoggedInUserInfoRepository LoggedInUserInfoRepository { set; protected get; }
    #endregion

    public static void Menu(IMenu menu)
    {
      menu.CreateSubMenu("Role")
        .AddAction("AddRole")
        .AddAction("ShowAllRoles");
      //menu.CreateSubMenu("FeatureType")
      //    .AddAction("AddFeatureType")
      //    .AddAction("ShowAllFeatureTypes");
      menu.CreateSubMenu("Feature")
        .AddAction("ShowFeatures");
    }

    #region ASP DOT NET MEMBERSHIP

    #region ROLE

    public NORole AddRole([RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
      string name)
    {
      NORole role = Container.NewTransientInstance<NORole>();

      role.Id = Guid.NewGuid().ToString();
      role.Name = name;

      Container.Persist(ref role);

      return role;
    }

    public string Validate0AddRole(string name)
    {
      NORole role = (from r in Container.Instances<NORole>()
                   where r.Name.ToLower() == name.ToLower()
                   select r).FirstOrDefault();

      if (role != null)
      {
        return "Duplicate Role";
      }

      return null;
    }

    //public bool HideAddRole()
    //{
    //  IList<NOFeature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

    //  NOFeature feature =
    //    features.Where(w => w.FeatureCode == (int)NOFeature.UserManagementEnum.AddRole
    //                        && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
    //      .FirstOrDefault();

    //  if (feature == null)
    //    return true;
    //  return false;
    //}

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Name")]
    public IQueryable<NORole> ShowAllRoles()
    {
      return Container.Instances<NORole>();
    }

    //public bool HideShowAllRoles()
    //{
    //  IList<NOFeature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

    //  NOFeature feature =
    //    features.Where(w => w.FeatureCode == (int)NOFeature.UserManagementEnum.ShowAllRoles
    //                        && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
    //      .FirstOrDefault();

    //  if (feature == null)
    //    return true;
    //  return false;
    //}

    #endregion

    #region Feature Type

    //public void AddFeatureType(string typeName)
    //{
    //    FeatureType feature = Container.NewTransientInstance<FeatureType>();

    //    feature.FeatureTypeName = typeName;

    //    Container.Persist(ref feature);
    //}
    //public bool HideAddFeatureType()
    //{
    //    IList<Feature> features = LoggedInUserInfoDomainRepository.GetFeatureListByLoginUser();

    //    Feature feature =
    //        features.Where(w => w.FeatureCode == (int)Feature.UserAccountsFeatureEnums.AddFeatureType
    //        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnums.UserAccount.ToString()).FirstOrDefault();

    //    if (feature == null)
    //        return true;
    //    return false;
    //}
    //[Eagerly(EagerlyAttribute.Do.Rendering)]
    //[TableView(true, "FeatureTypeName")]
    //public IQueryable<FeatureType> ShowAllFeatureTypes()
    //{
    //    return Container.Instances<FeatureType>();
    //}
    //public bool HideShowAllFeatureTypes()
    //{
    //    IList<Feature> features = LoggedInUserInfoDomainRepository.GetFeatureListByLoginUser();

    //    Feature feature =
    //        features.Where(w => w.FeatureCode == (int)Feature.UserAccountsFeatureEnums.ShowAllFeatureTypes
    //        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnums.UserAccount.ToString()).FirstOrDefault();

    //    if (feature == null)
    //        return true;
    //    return false;
    //}

    #endregion

    #region Feature

    //public void AddFeature(string featureName, FeatureType featureType)
    //{
    //    Feature feature = Container.NewTransientInstance<Feature>();

    //    feature.FeatureName = featureName;
    //    feature.FeatureType = featureType;

    //    Container.Persist(ref feature);
    //}
    //public bool HideAddFeature()
    //{
    //    IList<Feature> features = LoggedInUserInfoDomainRepository.GetFeatureListByLoginUser();

    //    Feature feature =
    //        features.Where(w => w.FeatureCode == (int)Feature.UserAccountsFeatureEnums.AddFeature
    //        && w.FeatureType.FeatureTypeName == FeatureType.FeatureTypeEnums.UserAccount.ToString()).FirstOrDefault();

    //    if (feature == null)
    //        return true;
    //    return false;
    //}

    public IList<NOFeature> ShowFeatures(NOFeatureType featureType)
    {
      IList<NOFeature> features =
        Container.Instances<NOFeature>().Where(w => w.FeatureType.FeatureTypeId == featureType.FeatureTypeId).ToList();

      return features;
    }

    public IList<NOFeatureType> Choices0ShowFeatures()
    {
      return Container.Instances<NOFeatureType>().ToList();
    }

    //public bool HideShowFeatures()
    //{
    //  IList<NOFeature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

    //  NOFeature feature =
    //    features.Where(w => w.FeatureCode == (int)NOFeature.UserManagementEnum.ShowFeatures
    //                        && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
    //      .FirstOrDefault();

    //  if (feature == null)
    //    return true;
    //  return false;
    //}

    #endregion

    #region Change Password

    public void ChangePassword([DataType(DataType.Password)] string oldPassword,
      [DataType(DataType.Password)] string newPassword,
      [DataType(DataType.Password)] string confirmNewPassword)
    {
      NOLoginUser user = GetLoggedinUser();
      user.PasswordHash = PasswordHash.HashPassword(newPassword);
      Container.InformUser("Password has been changed.");
    }

    public string ValidateChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
    {
      NOLoginUser user = GetLoggedinUser();
      //string enteredOldPassword = PasswordHash.HashPassword(oldPassword);

      if (!PasswordHash.VerifyHashedPassword(user.PasswordHash, oldPassword)) return "Old Password Does Not Match";
      if (newPassword != confirmNewPassword)
      {
        return "Password does not match";
      }

      return null;
    }

    public bool HideChangePassword()
    {
      IList<NOFeature> features = LoggedInUserInfoRepository.GetFeatureListByLoginUser();

      NOFeature feature =
        features.Where(w => w.FeatureCode == (int)NOFeature.UserManagementEnum.ChangePassword
                            && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
          .FirstOrDefault();

      if (feature == null)
        return true;
      return false;
    }

    private NOLoginUser GetLoggedinUser()
    {
      NOLoginUser user = Container.Instances<NOLoginUser>().Where(w => w.Email == Container.Principal.Identity.Name)
        .FirstOrDefault();

      return user;
    }

    #endregion
    #endregion
  }
}
