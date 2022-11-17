using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class GetStudentAddressResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public StudentAddress Address { get; set; }

    public GetStudentAddressResponseMessage(ValidationResult validationResult, StudentAddress studentAddress)
    {
      ValidationResult = validationResult;
      Address = studentAddress;
    }
    
    public class StudentAddress
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