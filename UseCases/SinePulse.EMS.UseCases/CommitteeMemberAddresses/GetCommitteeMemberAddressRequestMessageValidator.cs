using FluentValidation;
using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.CommitteeMemberAddresses
{
  public class GetCommitteeMemberAddressRequestMessageValidator : AbstractValidator<GetCommitteeMemberAddressRequestMessage>
  {
    public GetCommitteeMemberAddressRequestMessageValidator()
    {

    }
  }
}
