using System.Collections.Generic;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public interface IGetUnpaidYearsUseCase
  {
    List<int> GetUnpaidYears(GetUnpaidYearsRequestMessage message);
  }
}