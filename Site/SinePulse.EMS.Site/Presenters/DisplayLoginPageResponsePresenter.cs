using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.LoginMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayLoginPageResponsePresenter
  {
    public LoginViewModel Handle(ValidatedData<DisplayLoginPageResponseMessage> message,
      LoginViewModel viewModel,
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
      }

      return viewModel;
    }
  }
}