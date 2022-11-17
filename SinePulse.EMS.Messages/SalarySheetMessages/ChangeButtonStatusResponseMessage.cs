using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class ChangeButtonStatusResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ButtonStatus Status { get; }

    public ChangeButtonStatusResponseMessage(ValidationResult validationResult, ButtonStatus status)
    {
      ValidationResult = validationResult;
      Status = status;
    }
    
    public class ButtonStatus
    {
      public bool DisableSaveButton { get; set; }
      public bool ShowAccPostPrintStateAndExportButton { get; set; }
      public bool DisablePostAccount { get; set; }
    }
  }
}