using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TransactionMessages;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddBankVoucherJournalTransactionRequestHandler
    : IRequestHandler<AddBankVoucherJournalTransactionRequestMessage,
      ValidatedData<AddBankVoucherJournalTransactionResponseMessage>>
  {
    private readonly AddBankVoucherJournalTransactionRequestMessageValidator _validator;
    private readonly IAddBankVoucherJournalTransactionUseCase _useCase;

    public AddBankVoucherJournalTransactionRequestHandler(AddBankVoucherJournalTransactionRequestMessageValidator validator,
      IAddBankVoucherJournalTransactionUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<AddBankVoucherJournalTransactionResponseMessage>> Handle(
      AddBankVoucherJournalTransactionRequestMessage request, CancellationToken cancellationToken)
    {
      ValidatedData<AddBankVoucherJournalTransactionResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddBankVoucherJournalTransactionResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddBankVoucherJournalTransaction(request);

      returnMessage = new ValidatedData<AddBankVoucherJournalTransactionResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}