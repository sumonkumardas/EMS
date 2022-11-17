
using MediatR;
using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.CommitteeMemberAddresses
{
  public class GetCommitteeMemberAddressRequestHandler : IRequestHandler<GetCommitteeMemberAddressRequestMessage, GetCommitteeMemberAddressResponseMessage>
  {
    private readonly GetCommitteeMemberAddressRequestMessageValidator _validator;
    private readonly IGetCommitteeMemberAddressUseCase _useCase;

    public GetCommitteeMemberAddressRequestHandler(GetCommitteeMemberAddressRequestMessageValidator validator,
      IGetCommitteeMemberAddressUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetCommitteeMemberAddressResponseMessage> Handle(GetCommitteeMemberAddressRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetCommitteeMemberAddressResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetCommitteeMemberAddressResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var committeeMemberAddress = _useCase.GetCommitteeMemberAddress(request);

      returnMessage = new GetCommitteeMemberAddressResponseMessage(validationResult, committeeMemberAddress);
      return Task.FromResult(returnMessage);
    }
  }
}
