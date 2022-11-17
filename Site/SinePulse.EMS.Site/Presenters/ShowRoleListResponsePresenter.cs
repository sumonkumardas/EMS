using System.Linq;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowRoleListResponsePresenter
  {
    public ShowRoleListViewModel Handle(ShowRoleListResponseMessage message, ShowRoleListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.Roles = message.Roles.Select(ToViewRole).ToList();
      return viewModel;
    }

    private ShowRoleListViewModel.Role ToViewRole(ShowRoleListResponseMessage.Role messageRole)
    {
      return new ShowRoleListViewModel.Role
      {
        RoleId = messageRole.RoleId,
        RoleName = messageRole.RoleName,
        Status = messageRole.Status,
        RoleType = messageRole.RoleType
      };
    }
  }
}