using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowShiftResponsePresenter
  {
    public ShiftViewModel Handle(ShowShiftResponseMessage message, ShiftViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.ShiftName = message.Shift.ShiftName;
      viewModel.ShiftId = message.Shift.Id;
      viewModel.StartTime = message.Shift.StartTime;
      viewModel.EndTime = message.Shift.EndTime;
      viewModel.Status = message.Shift.Status;
      viewModel.BranchId = message.Shift.Branch.Id;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return viewModel;
    }
  }
}