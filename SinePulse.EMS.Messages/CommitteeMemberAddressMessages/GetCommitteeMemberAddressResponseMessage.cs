using FluentValidation.Results;

namespace SinePulse.EMS.Messages.CommitteeMemberAddressMessages
{
  public class GetCommitteeMemberAddressResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public CommitteeMemberAddress Address { get; set; }

    public GetCommitteeMemberAddressResponseMessage(ValidationResult validationResult, CommitteeMemberAddress committerMemberAddress)
    {
      ValidationResult = validationResult;
      Address = committerMemberAddress;
    }

    public class CommitteeMemberAddress
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
