using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowStudentAttendanceResponsePresenter
  {
    public StudentAttendanceViewModel Handle(ShowStudentAttendanceResponseMessage message,
      StudentAttendanceViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Id = message.StudentAttendance.Id;
      viewModel.AttendanceDate = message.StudentAttendance.AttendanceDate;
      viewModel.InTime = message.StudentAttendance.InTime;
      viewModel.OutTime = message.StudentAttendance.OutTime;

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