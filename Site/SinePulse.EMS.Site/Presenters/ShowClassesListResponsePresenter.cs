using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowClassesListResponsePresenter
  {
    public ShowClassesListViewModel Handle(ShowClassesListResponseMessage message, ShowClassesListViewModel viewModel, 
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Classes = message.Classes;
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