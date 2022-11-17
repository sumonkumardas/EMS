using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.CommitteeMemberAddresses
{
  public interface IAddCommitteeMemberAddressUseCase
  {
    void AddCommitteeMemberAddress(AddCommitteeMemberAddressRequestMessage message);
  }
}
