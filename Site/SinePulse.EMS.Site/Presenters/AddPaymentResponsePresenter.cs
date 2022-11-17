using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class AddPaymentResponsePresenter
  {
    public PayBillViewModel Handle(AddPaymentResponseMessage message, PayBillViewModel viewModel,
      ModelStateDictionary modelState)
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
        viewModel = new PayBillViewModel();
      }

      return viewModel;
    }
  }
}