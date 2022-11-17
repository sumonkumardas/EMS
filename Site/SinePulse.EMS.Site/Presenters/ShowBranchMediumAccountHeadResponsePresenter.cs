using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowBranchMediumAccountHeadResponsePresenter
    {
    public AddAccountHeadViewModel Handle(AddAccountHeadResponseMessage message, AddAccountHeadViewModel viewModel,
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
        viewModel = new AddAccountHeadViewModel();
      }

      return viewModel;
    }
  }
}