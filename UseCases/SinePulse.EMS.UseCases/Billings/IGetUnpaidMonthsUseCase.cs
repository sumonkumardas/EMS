using System.Collections.Generic;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public interface IGetUnpaidMonthsUseCase
  {
    List<GetUnpaidMonthsResponseMessage.Month> GetUnpaidMonths(GetUnpaidMonthsRequestMessage message);
  }
}