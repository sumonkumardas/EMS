using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowSeatPlanResponsePresenter
  {
    public SeatPlanViewModel Handle(ShowSeatPlanResponseMessage message, SeatPlanViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.SeatPlanId = message.SeatPlanData.Id;
      viewModel.RollRange = message.SeatPlanData.RollRange;
      viewModel.TotalStudent = message.SeatPlanData.TotalStudent;
      viewModel.RoomData = new SeatPlanViewModel.Room
      {
        RoomId = message.SeatPlanData.Room.RoomId,
        RoomNo = message.SeatPlanData.Room.RoomNo
      };
      viewModel.Status = message.SeatPlanData.Status;

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