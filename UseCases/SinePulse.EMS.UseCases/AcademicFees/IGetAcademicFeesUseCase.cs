using System.Collections.Generic;
using SinePulse.EMS.Messages.AcademicFeeMessages;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public interface IGetAcademicFeesUseCase
  {
    IEnumerable<AcademicFeeMessageModel> GetAcademicFees(GetAcademicFeesRequestMessage message);
  }
}