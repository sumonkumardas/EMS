using System.Collections.Generic;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public interface IShowAcademicAccountHeadListUseCase
  {
    IEnumerable<AccountHeadMessageModel> ShowAcademicAccountHeadList(ShowAcademicAccountHeadListRequestMessage message);
  }
}