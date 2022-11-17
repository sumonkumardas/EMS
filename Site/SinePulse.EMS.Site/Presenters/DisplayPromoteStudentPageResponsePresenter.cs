using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.StudentPromotionMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayPromoteStudentsPageResponsePresenter
  {
    public PromoteStudentsViewModel Handle(ValidatedData<DisplayPromoteStudentsPageResponseMessage> message,
      PromoteStudentsViewModel viewModel, ModelStateDictionary modelState,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      if (!message.ValidationResult.IsValid)
      {
        modelState.Clear();
        foreach (var error in message.ValidationResult.Errors)
        {
          modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
      }
      else
      {
        modelState.Clear();
      }

      viewModel.CurrentSectionStudents = message.Data.CurrentSectionStudents.Select(ToViewModelStudent).ToList();
      viewModel.NextSessionSections = message.Data.NextSessionSections.Select(ToViewModelSection).ToList();
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

    private static PromoteStudentsViewModel.Student ToViewModelStudent(
      DisplayPromoteStudentsPageResponseMessage.Student student)
    {
      return new PromoteStudentsViewModel.Student
      {
        StudentId = student.StudentId,
        StudentName = student.StudentName,
        StudentRoll = student.StudentRoll,
        StudentSectionId = student.StudentSectionId
      };
    }

    private static PromoteStudentsViewModel.Section ToViewModelSection(
      DisplayPromoteStudentsPageResponseMessage.Section section)
    {
      return new PromoteStudentsViewModel.Section
      {
        SectionId = section.SectionId,
        SectionText = $"{section.SectionName} - {section.AvailableSeats} seat(s) available"
      };
    }
  }
}