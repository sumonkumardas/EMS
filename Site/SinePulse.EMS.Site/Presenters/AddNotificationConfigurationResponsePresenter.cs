using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class AddNotificationConfigurationResponsePresenter
  {
    public AddNotificationConfigurationViewModel Handle(AddNotificationConfigurationResponseMessage message, AddNotificationConfigurationViewModel viewModel,
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
        viewModel = new AddNotificationConfigurationViewModel();
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