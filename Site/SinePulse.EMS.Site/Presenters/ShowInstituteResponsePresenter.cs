using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowInstituteResponsePresenter
  {
    public ShowInstituteViewModel Handle(ShowInstituteResponseMessage message, ShowInstituteViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Institute = message.Institute;
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