using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.RegisterMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class RegisterResponsePresenter
  {
    public RegisterViewModel Handle(ValidatedData<RegisterResponseMessage> message, RegisterViewModel viewModel,
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
        viewModel = new RegisterViewModel();
      }

      return viewModel;
    }
  }
}