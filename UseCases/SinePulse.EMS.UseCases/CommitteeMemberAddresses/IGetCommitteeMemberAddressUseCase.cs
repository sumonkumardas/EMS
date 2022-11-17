using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.CommitteeMemberAddresses
{
  public interface IGetCommitteeMemberAddressUseCase
  {
    GetCommitteeMemberAddressResponseMessage.CommitteeMemberAddress GetCommitteeMemberAddress(GetCommitteeMemberAddressRequestMessage message);
  }
}
