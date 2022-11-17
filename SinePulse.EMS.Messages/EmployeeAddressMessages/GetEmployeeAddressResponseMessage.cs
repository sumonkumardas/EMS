using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeAddressMessages
{
  public class GetEmployeeAddressResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EmployeeAddress Address { get; set; }

    public GetEmployeeAddressResponseMessage(ValidationResult validationResult, EmployeeAddress employeeAddress)
    {
      ValidationResult = validationResult;
      Address = employeeAddress;
    }
    
    public class EmployeeAddress
    {
      public string PresentStreet1 { get; set; }
      public string PresentStreet2 { get; set; }
      public string PresentPostalCode { get; set; }
      public string PresentCity { get; set; }
    
      public string PermanentStreet1 { get; set; }
      public string PermanentStreet2 { get; set; }
      public string PermanentPostalCode { get; set; }
      public string PermanentCity { get; set; }
    }
  }
}