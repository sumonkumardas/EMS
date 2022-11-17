using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class AddTermTestMarksResponsePresenter
  {
    public AddTermTestMarksViewModel Handle(AddTermTestMarksResponseMessage message,
      AddTermTestMarksViewModel viewModel,
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
        viewModel = new AddTermTestMarksViewModel();
      }
      return viewModel;
    }
  }
}