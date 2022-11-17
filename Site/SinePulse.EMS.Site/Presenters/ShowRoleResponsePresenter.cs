using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowRoleResponsePresenter
  {
    public RoleViewModel Handle(ShowRoleResponseMessage message, RoleViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.RoleId = message.RoleData.RoleId;
      viewModel.RoleName = message.RoleData.RoleName;
      viewModel.RoleType = message.RoleData.RoleType;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return viewModel;
    }
  }
}