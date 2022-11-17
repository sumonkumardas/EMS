using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayEditStudentPageResponsePresenter
  {
    public EditStudentViewModel Handle(StudentViewModel model,
      EditStudentViewModel viewModel, 
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Id = model.Id;
      viewModel.BranchMediumId = model.BranchMediumId;
      viewModel.FullName = model.FullName;
      viewModel.Guardian = (RelationTypeEnum) model.Guardian;
      viewModel.BirthDate = model.BirthDate;
      viewModel.Email = model.Email;
      viewModel.MobileNo = model.MobileNo;
      viewModel.Group = (GroupEnum) model.Group;
      viewModel.ClassId = model.ClassId;
      viewModel.SectionId = model.SectionId.ToString();
      viewModel.RollNo = model.Roll;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;
      return viewModel;
    }
  }
}