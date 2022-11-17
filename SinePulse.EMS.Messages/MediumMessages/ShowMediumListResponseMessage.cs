using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class ShowMediumListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<MediumMessageModel> Mediums { get; }

    public ShowMediumListResponseMessage(ValidationResult validationResult, IEnumerable<MediumMessageModel> mediums)
    {
      ValidationResult = validationResult;
      Mediums = mediums;
    }
  }
}