using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Messages.CommitteeMemberAddressMessages
{
  public class GetCommitteeMemberAddressRequestMessage : IRequest<GetCommitteeMemberAddressResponseMessage>
  {
    public long CommitteeMemberId { get; set; }
  }
}
