using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowResultGradeResponsePresenter
  {
    public ResultGradeViewModel Handle(ShowResultGradeResponseMessage message, ResultGradeViewModel viewModel, 
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Id = message.ResultGradeData.Id;
      viewModel.GradeLetter = message.ResultGradeData.GradeLetter;
      viewModel.GradePoint = message.ResultGradeData.GradePoint;
      viewModel.StartMark = message.ResultGradeData.StartMark;
      viewModel.EndMark = message.ResultGradeData.EndMark;
      viewModel.Status = message.ResultGradeData.Status;
      viewModel.BranchMedium = message.ResultGradeData.BranchMedium;

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