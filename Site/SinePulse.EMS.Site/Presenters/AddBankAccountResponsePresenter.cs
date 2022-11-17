using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class AddBankAccountResponsePresenter
  {
    public AddBankAccountViewModel Handle(AddBankAccountResponseMessage message, AddBankAccountViewModel viewModel,
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
        viewModel = new AddBankAccountViewModel();
      }

      return viewModel;
    }
  }
}