using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class GetBankStatementInfoRequestHandler : IRequestHandler<GetBankStatementInfoRequestMessage, GetBankStatementInfoResponseMessage>
  {
    private readonly GetBankStatementInfoRequestMessageValidator _validator;
    private readonly IGetBankStatementInfoUseCase _useCase;

    public GetBankStatementInfoRequestHandler(GetBankStatementInfoRequestMessageValidator validator, IGetBankStatementInfoUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetBankStatementInfoResponseMessage> Handle(GetBankStatementInfoRequestMessage request, CancellationToken cancellationToken)
    {
      GetBankStatementInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetBankStatementInfoResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bankStatementInfo = _useCase.GetBankStatementInfo(request);

      returnMessage = new GetBankStatementInfoResponseMessage(validationResult, bankStatementInfo);
      return Task.FromResult(returnMessage);
    }
  }
}