using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayAddSeatPlanPageResponsePresenter
  {
    public AddSeatPlanViewModel Handle(ValidatedData<DisplayAddSeatPlanPageResponseMessage> message, AddSeatPlanViewModel viewModel,
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
        viewModel.RemainingStudent = message.Data.RemainingStudent;
        viewModel.Rooms = message.Data.Rooms.Select(ToViewRoom).ToList();
        viewModel.TermId = message.Data.ExamTermId;
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

    private AddSeatPlanViewModel.Room ToViewRoom(DisplayAddSeatPlanPageResponseMessage.Room responseRoom)
    {
      return new AddSeatPlanViewModel.Room
      {
        RoomId = responseRoom.RoomId,
        RoomText =
          $"{responseRoom.RoomNo} ({responseRoom.AvailableSeat} seats are available out of {responseRoom.TotalSeat} seats)"
      };
    }
  }
}