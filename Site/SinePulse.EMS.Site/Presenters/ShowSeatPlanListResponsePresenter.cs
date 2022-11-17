using System.Collections.Generic;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowSeatPlanListResponsePresenter
  {
    public ShowSeatPlanListViewModel Handle(ShowSeatPlanListResponseMessage message, ShowSeatPlanListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.SeatPlans = new List<SeatPlanViewModel>();

      foreach (var seatPlan in message.SeatPlanData)
      {
        var model = new SeatPlanViewModel
        {
          SeatPlanId = seatPlan.Id,
          RollRange = seatPlan.RollRange,
          TotalStudent = seatPlan.TotalStudent,
          RoomData = new SeatPlanViewModel.Room {RoomId = seatPlan.Room.RoomId, RoomNo = seatPlan.Room.RoomNo},
          TestData = new SeatPlanViewModel.Test {TestId = seatPlan.Test.TestId, TestStartDate = seatPlan.Test.TestStartDate,TestEndDate = seatPlan.Test.TestEndDate,ClassName = seatPlan.Test.ClassName,SubjectName = seatPlan.Test.SubjectName,ExamType = seatPlan.Test.ExamType}
        };

        viewModel.SeatPlans.Add(model);
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
  }
}