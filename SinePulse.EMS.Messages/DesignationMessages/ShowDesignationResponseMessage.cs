using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class ShowDesignationResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public DesignationMessageModel Designation { get; }

    public ShowDesignationResponseMessage(ValidationResult validationResult, DesignationMessageModel designation)
    {
      ValidationResult = validationResult;
      Designation = designation;
    }
  }
}