using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.LoginUserMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class AddLoginUserResponsePresenter
  {
    public AddLoginUserViewModel Handle(AddLoginUserResponseMessage message, AddLoginUserViewModel viewModel,
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
        viewModel = new AddLoginUserViewModel();
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