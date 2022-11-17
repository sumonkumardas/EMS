using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowCommitteeMemberResponsePresenter
  {
    public ShowCommitteeMemberViewModel Handle(ShowCommitteeMemberResponseMessage message,
      ShowCommitteeMemberViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.CommitteeMember = message.CommitteeMember;
      return viewModel;
    }
  }
}