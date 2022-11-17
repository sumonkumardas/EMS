using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayEditSeatPlanPageResponsePresenter
  {
    public EditSeatPlanViewModel Handle(ValidatedData<DisplayEditSeatPlanPageResponseMessage> message, EditSeatPlanViewModel viewModel,
      ModelStateDictionary modelState, GetUserAssociationResponseMessage userAssociationMessage)
    {
      if (!message.ValidationResult.IsValid)
      {
        foreach (var error in message.ValidationResult.Errors)
        {
          modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
      }
      else
      {
        modelState.Clear();
        viewModel.TermId = message.Data.TermId;
        viewModel.SeatPlanId = message.Data.SeatPlanData.Id;
        viewModel.RollRange = message.Data.SeatPlanData.RollRange;
        viewModel.TotalStudent = message.Data.SeatPlanData.TotalStudent;
        viewModel.RoomId = message.Data.SeatPlanData.RoomId;
        viewModel.TestId = message.Data.SeatPlanData.TestId;
        viewModel.RemainingStudent = message.Data.RemainingStudent;
        viewModel.Rooms = message.Data.Rooms.Select(ToViewRoom).ToList();
      }
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

    private EditSeatPlanViewModel.Room ToViewRoom(DisplayEditSeatPlanPageResponseMessage.Room responseRoom)
    {
      return new EditSeatPlanViewModel.Room
      {
        RoomId = responseRoom.RoomId,
        RoomText =
          $"{responseRoom.RoomNo} ({responseRoom.AvailableSeat} seats are available out of {responseRoom.TotalSeat} seats)"
      };
    }
  }
}