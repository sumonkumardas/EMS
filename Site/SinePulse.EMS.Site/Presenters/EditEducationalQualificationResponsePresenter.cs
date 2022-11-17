using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class EditEducationalQualificationResponsePresenter
  {
    public EducationalQualificationViewModel Handle(EditEducationalQualificationResponseMessage message,
      EducationalQualificationViewModel viewModel,
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
        viewModel = new EducationalQualificationViewModel();
      }

      return viewModel;
    }
  }
}