using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.OtherComponentMessages
{
  public class GetOtherComponentsListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<OtherComponent> OtherComponents { get; }

    public GetOtherComponentsListResponseMessage(ValidationResult validationResult, List<OtherComponent> otherComponents)
    {
      ValidationResult = validationResult;
      OtherComponents = otherComponents;
    }
    
    public class OtherComponent
    {
      public string ComponentName { get; set; }
      public long ComponentId { get; set; }
    }
  }
}