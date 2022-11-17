using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class ImportCOAFromMasterRequestMessage : IRequest<ImportCOAFromMasterResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public long SessionId { get; set; }
    public IEnumerable<AccountHeadMessageModel> AccountsHeads { get; set; }
    public string CurrentUserName { get; set; }
  }
}