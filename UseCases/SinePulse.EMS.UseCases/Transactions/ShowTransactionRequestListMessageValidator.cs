using FluentValidation;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class ShowTransactionListRequestMessageValidator : AbstractValidator<ShowTransactionListRequestMessage>
  {
  }
}