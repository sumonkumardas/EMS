using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowStudentResponsePresenter
  {
    public StudentViewModel Handle(ShowStudentResponseMessage message, StudentViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.ImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;

      viewModel.Id = message.StudentData.StudentId;
      viewModel.FullName = message.StudentData.FullName;
      viewModel.BirthDate = message.StudentData.BirthDate;
      viewModel.Guardian = message.StudentData.Guardian;
      viewModel.Email = message.StudentData.Email;
      viewModel.MobileNo = message.StudentData.MobileNo;
      viewModel.Status = message.StudentData.Status;
      viewModel.ImageUrl = message.StudentData.ImageUrl;
      viewModel.LoginUsersInstituteId = message.StudentData.InstituteId;
      viewModel.InstituteName = message.StudentData.InstituteName;
      viewModel.LoginUsersBranchId = message.StudentData.BranchId;
      viewModel.BranchId = message.StudentData.BranchId;
      viewModel.BranchName = message.StudentData.BranchName;
      viewModel.MediumId = message.StudentData.MediumId;
      viewModel.MediumName = message.StudentData.MediumName;
      viewModel.Group = message.StudentData.Group;
      viewModel.ClassId = message.StudentData.ClassId;
      viewModel.ClassName = message.StudentData.ClassName;
      viewModel.SectionId = message.StudentData.SectionId;
      viewModel.SectionName = message.StudentData.SectionName;
      viewModel.Roll = message.StudentData.Roll;
      viewModel.LoginUsersBranchMediumId = message.StudentData.BranchMediumId;
      viewModel.InstituteId = message.StudentData.InstituteId;
      viewModel.ShiftName = message.StudentData.ShiftName;
      return viewModel;
    }
  }
}