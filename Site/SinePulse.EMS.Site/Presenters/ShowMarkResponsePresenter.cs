using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Messages.AuthorizationMessages;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowMarkResponsePresenter
  {
    public MarkViewModel Handle(ShowMarkResponseMessage message, MarkViewModel viewModel, 
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.MarkId = message.MarkData.Id;
      viewModel.ObtainedMark = message.MarkData.ObtainedMark;
      viewModel.GraceMark = message.MarkData.GraceMark;
      viewModel.Comment = message.MarkData.Comment;
      viewModel.Status = message.MarkData.Status;
      viewModel.StudentData = new MarkViewModel.Student
      {
        StudentSectionId = message.MarkData.Student.StudentSectionId,
        RollNo = message.MarkData.Student.RollNo,
        StudentName = message.MarkData.Student.StudentName ,
        ClassName = message.MarkData.Student.ClassName ,
        ShiftName = message.MarkData.Student.ShiftName ,
        SectionName = message.MarkData.Student.SectionName
      };

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