using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayImportSessionDataPageResponsePresenter
  {
    public ImportSessionDataViewModel Handle(ValidatedData<DisplayImportSessionDataPageResponseMessage> message,
      ImportSessionDataViewModel viewModel, ModelStateDictionary modelState,
      GetUserAssociationResponseMessage userAssociationMessage)
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
        viewModel.CurrentSession = ToViewModelSession(message.Data.CurrentSession);
        viewModel.PreviousSessions = message.Data.PreviousSessions.Select(ToViewModelSession).ToList();
      }

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;
      return viewModel;
    }

    private ImportSessionDataViewModel.Session ToViewModelSession(
      DisplayImportSessionDataPageResponseMessage.Session responseSession)
    {
      return new ImportSessionDataViewModel.Session
      {
        SessionId = responseSession.SessionId,
        SessionText = responseSession.SessionName
      };
    }
  }
}