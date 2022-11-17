using FluentValidation.Results;
using SinePulse.EMS.Domain.Academic;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.InstituteMessages
{
  public class ShowInstituteListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<Institute> InstituteList { get; }

    public ShowInstituteListResponseMessage(ValidationResult validationResult, List<Institute> instituteList)
    {
      ValidationResult = validationResult;
      InstituteList = instituteList;
    }
  }
}