using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.StudentPromotionMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayPromoteStudentOptionPageResponsePresenter
  {
    public PromoteStudentOptionViewModel Handle(ValidatedData<DisplayPromoteStudentOptionPageResponseMessage> message,
      PromoteStudentOptionViewModel viewModel, ModelStateDictionary modelState,
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

      viewModel.Sessions = message.Data.Sessions.Select(ToViewModelSession).ToList();
      viewModel.Classes = message.Data.Classes.Select(ToViewModelClass).ToList();
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

    private static PromoteStudentOptionViewModel.Class ToViewModelClass(
      DisplayPromoteStudentOptionPageResponseMessage.Class @class)
    {
      return new PromoteStudentOptionViewModel.Class
      {
        ClassId = @class.ClassId,
        ClassName = @class.ClassName
      };
    }

    private static PromoteStudentOptionViewModel.Session ToViewModelSession(
      DisplayPromoteStudentOptionPageResponseMessage.Session section)
    {
      return new PromoteStudentOptionViewModel.Session
      {
        SessionId = section.SessionId,
        SessionName = section.SessionName
      };
    }
  }
}