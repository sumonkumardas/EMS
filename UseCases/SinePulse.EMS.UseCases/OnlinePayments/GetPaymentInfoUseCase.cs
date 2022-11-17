using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.PaymentGateway;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class GetPaymentInfoUseCase : IGetPaymentInfoUseCase
  {
    private readonly IRepository _repository;

    public GetPaymentInfoUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetPaymentInfoResponseMessage.PaymentInfo GetPaymentInfo(GetPaymentInfoRequestMessage message)
    {
      var paymentInfo = _repository.Table<SslPaymentGateway>()
        .Include(nameof(BranchMedium))
        .FirstOrDefault(p => p.TransactionId == message.TransactionId);

      if (paymentInfo != null)
      {
        return new GetPaymentInfoResponseMessage.PaymentInfo
        {
          BranchMediumId = paymentInfo.BranchMedium.Id,
          Month = paymentInfo.Month,
          TransactionId = paymentInfo.TransactionId,
          UserCode = paymentInfo.UserCode,
          Year = paymentInfo.Year,
          PaymentDate = paymentInfo.PaymentDate,
          PerStudentCharge = paymentInfo.PerStudentCharge,
          TotalStudents = paymentInfo.TotalStudents,
          TransactionAmount = paymentInfo.TransactionAmount,
          StoreAmount = paymentInfo.StoreAmount,
          ValidationId = paymentInfo.ValidationId,
          CardType = paymentInfo.CardType
        };
      }

      return new GetPaymentInfoResponseMessage.PaymentInfo();
    }
  }
}