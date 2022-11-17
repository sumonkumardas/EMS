using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowSessionListResponsePresenter
  {
    public ShowSessionListViewModel Handle(ShowSessionListResponseMessage message, ShowSessionListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Sessions = message.Sessions;
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;
      return viewModel;
    }
  }
}