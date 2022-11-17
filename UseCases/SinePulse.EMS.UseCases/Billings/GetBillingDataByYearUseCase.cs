using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SinePulse.EMS.Domain.Billing;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetBillingDataByYearUseCase : IGetBillingDataByYearUseCase
  {
    private readonly IRepository _repository;

    public GetBillingDataByYearUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetBillingDataByYearResponseMessage.Billing GetBillingDataByYear(GetBillingDataByYearRequestMessage message)
    {
      var billingData = _repository.Table<BillingData>()
        .Where(b => b.BranchMedium.Id == message.BranchMediumId 
                    && b.Year == message.Year)
        .OrderByDescending(b => b.Month)
        .ToList();

      return ToRequestBillingData(billingData);
    }

    private GetBillingDataByYearResponseMessage.Billing ToRequestBillingData(List<BillingData> billingData)
    {
      var requestData = new GetBillingDataByYearResponseMessage.Billing();
      var billingInfo = new List<GetBillingDataByYearResponseMessage.BillingInfo>();
      foreach (var data in billingData)
      {
        billingInfo.Add(new GetBillingDataByYearResponseMessage.BillingInfo
        {
          TransactionNo = data.TransactionId,
          UserCode = data.UserCode,
          Year = data.Year,
          Month = data.Month.ToString("G"),
          PaymentDate = data.PaymentDate.ToString("dd-MMM-yyy"),
          PerStudentCharge = data.PerStudentCharge.ToString(CultureInfo.InvariantCulture),
          TotalStudents = data.TotalStudents,
          Amount = data.Amount.ToString(),
          Vat = data.Vat.ToString(),
          PaymentMethod = data.PaymentMethod.ToString("G"),
          IsProcessed = data.IsProcessed
        });
      }

      requestData.BillingInfos = billingInfo;
      return requestData;
    }
  }
}