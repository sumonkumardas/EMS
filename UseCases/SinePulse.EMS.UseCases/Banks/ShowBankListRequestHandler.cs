using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class ShowBankListRequestHandler : IRequestHandler<ShowBankListRequestMessage, ShowBankListResponseMessage>
  {
    private readonly ShowBankListRequestMessageValidator _validator;
    private readonly IShowBankListUseCase _useCase;

    public ShowBankListRequestHandler(ShowBankListRequestMessageValidator validator, IShowBankListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowBankListResponseMessage> Handle(ShowBankListRequestMessage request, CancellationToken cancellationToken)
    {
      ShowBankListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBankListResponseMessage(validationResult, new List<ShowBankListResponseMessage.Bank>());
        return Task.FromResult(returnMessage);
      }

      var bankInfoList = _useCase.GetBankList(request);

      returnMessage = new ShowBankListResponseMessage(validationResult, bankInfoList);
      return Task.FromResult(returnMessage);
    }
  }
}