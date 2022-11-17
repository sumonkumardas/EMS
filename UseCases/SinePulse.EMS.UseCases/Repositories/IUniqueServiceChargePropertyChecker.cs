using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueServiceChargePropertyChecker
  {
    bool IsUniqueEffectiveDate(DateTime effectiveDate, long branchMediumId);
    bool IsUniqueEffectiveDate(DateTime effectiveDate, long branchMediumId, long ServiceChargeId);
  }
}
