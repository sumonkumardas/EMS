using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowClassResponsePresenter
  {
    public ClassViewModel Handle(ShowClassResponseMessage message, ClassViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.ClassName = message.Class.ClassName;
      viewModel.Id = message.Class.Id;
      viewModel.ClassCode = message.Class.ClassCode;
      viewModel.Status = (Messages.Model.Enums.StatusEnum)message.Class.Status;

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