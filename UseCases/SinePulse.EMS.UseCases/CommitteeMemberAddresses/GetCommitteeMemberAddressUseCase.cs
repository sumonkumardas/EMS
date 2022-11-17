using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.CommitteeMemberAddresses
{
  public class GetCommitteeMemberAddressUseCase : IGetCommitteeMemberAddressUseCase
  {
    private readonly IRepository _repository;

    public GetCommitteeMemberAddressUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetCommitteeMemberAddressResponseMessage.CommitteeMemberAddress GetCommitteeMemberAddress(
      GetCommitteeMemberAddressRequestMessage message)
    {
      var committeeMember = _repository.GetByIdWithInclude<CommitteeMember>(message.CommitteeMemberId,
        new[] { nameof(CommitteeMember.PresentAddress), nameof(CommitteeMember.PermanentAddress) });

      if (committeeMember.PermanentAddress != null && committeeMember.PresentAddress != null)
      {
        return new GetCommitteeMemberAddressResponseMessage.CommitteeMemberAddress
        {
          PresentStreet1 = committeeMember.PresentAddress.Street1,
          PresentStreet2 = committeeMember.PresentAddress.Street2,
          PresentPostalCode = committeeMember.PresentAddress.PostalCode,
          PresentCity = committeeMember.PresentAddress.City,
          PermanentStreet1 = committeeMember.PermanentAddress.Street1,
          PermanentStreet2 = committeeMember.PermanentAddress.Street2,
          PermanentPostalCode = committeeMember.PermanentAddress.PostalCode,
          PermanentCity = committeeMember.PermanentAddress.City
        };
      }

      return new GetCommitteeMemberAddressResponseMessage.CommitteeMemberAddress();
    }
  }
}
