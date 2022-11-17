using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Messages.CommitteeMemberAddressMessages
{
  public class AddCommitteeMemberAddressResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddCommitteeMemberAddressResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}
