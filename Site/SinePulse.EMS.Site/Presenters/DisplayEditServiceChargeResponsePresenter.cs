using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayEditServiceChargeResponsePresenter
  {
    public EditServiceChargeViewModel Handle(ValidatedData<DisplayEditServiceChargeResponseMessage>  message, EditServiceChargeViewModel viewModel,
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
        viewModel.ServiceChargeId = message.Data.Charge.Id;
        viewModel.PaymentBufferPeriod = message.Data.Charge.PaymentBufferPeriod;
        viewModel.PerStudentCharge = message.Data.Charge.PerStudentCharge;
        viewModel.EffectiveDate = message.Data.Charge.EffectiveDate;
        viewModel.EndDate = message.Data.Charge.EndDate;
        viewModel.BranchMedium = message.Data.Charge.BranchMedium;
      }

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return viewModel;
    }
  }
}
