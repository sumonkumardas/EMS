using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayAddExamTypePageResponsePresenter
  {
    public AddExamConfigurationViewModel Handle(ValidatedData<DisplayAddExamConfigurationPageResponseMessage> message,
      AddExamConfigurationViewModel viewModel,
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
        viewModel.Classes = message.Data.ClassSubjects.Select(ToViewModelGroupSubject).ToList();
      }

      return viewModel;
    }

    private static AddExamConfigurationViewModel.Class ToViewModelGroupSubject(
      DisplayAddExamConfigurationPageResponseMessage.ClassSubject responseGroupSubject)
    {
      return new AddExamConfigurationViewModel.Class
      {
        ClassId = responseGroupSubject.ClassSubjectId,
        ClassName =
          $"{responseGroupSubject.SubjectName} - [{responseGroupSubject.ClassName} - {responseGroupSubject.Group}]"
      };
    }
  }
}