using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowMediumResponsePresenter
  {
    public MediumViewModel Handle(ShowMediumResponseMessage message, MediumViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.MediumName = message.Medium.MediumName;
      viewModel.MediumId = message.Medium.Id;
      viewModel.MediumCode = message.Medium.MediumCode;
      viewModel.Status = (Messages.Model.Enums.StatusEnum)message.Medium.Status;

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