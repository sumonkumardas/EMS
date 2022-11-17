using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class ShowClassesListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<ClassMessageModel> Classes { get; }

    public ShowClassesListResponseMessage(ValidationResult validationResult, IEnumerable<ClassMessageModel> classes)
    {
      ValidationResult = validationResult;
      Classes = classes;
    }
  }
}