using MediatR;
using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos
{
  public class GetCommitteeMemberPersonalInfoRequestHandler : IRequestHandler<GetCommitteeMemberPersonalInfoRequestMessage,
    GetCommitteeMemberPersonalInfoResponseMessage>
  {
    private readonly GetCommitteeMemberPersonalInfoRequestMessageValidator _validator;
    private readonly IGetCommitteeMemberPersonalInfoUseCase _useCase;

    public GetCommitteeMemberPersonalInfoRequestHandler(GetCommitteeMemberPersonalInfoRequestMessageValidator validator,
      IGetCommitteeMemberPersonalInfoUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetCommitteeMemberPersonalInfoResponseMessage> Handle(GetCommitteeMemberPersonalInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetCommitteeMemberPersonalInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetCommitteeMemberPersonalInfoResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeePersonalInfo = _useCase.GetCommitteeMemberPersonalInfo(request);

      returnMessage = new GetCommitteeMemberPersonalInfoResponseMessage(validationResult, employeePersonalInfo);
      return Task.FromResult(returnMessage);
    }
  }
}
