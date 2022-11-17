using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class SaveSalarySheetResponsePresenter
  {                                                         
    public SaveSalarySheetViewModel Handle(SaveSalarySheetResponseMessage message,
      SaveSalarySheetViewModel viewModel,
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
        viewModel = new SaveSalarySheetViewModel();
      }

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      return viewModel;
    }
  }
}