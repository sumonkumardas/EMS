using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class GetBankAccountAccountHeadsUseCase : IGetBankAccountAccountHeadsUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public GetBankAccountAccountHeadsUseCase(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
      _mapperConfiguration = new MapperConfiguration(cfg =>
        cfg.CreateMap<BranchMediumAccountHead, BranchMediumAccountsHeadMessageModel>());
    }

    public IEnumerable<BranchMediumAccountsHeadMessageModel> GetBankAccountAccountHeads(
      GetBankAccountAccountHeadsRequestMessage message)
    {
      var bankAccountCode = _repository.Table<AutoPostingConfiguration>()
        .FirstOrDefault(a => a.VoucherType == VoucherTypeEnum.BankVoucher)
        ?.AccountCode;
      var currentSession = _currentSessionProvider.GetCurrentSession(message.BranchMediumId);
      var bankParentAccountHead = _repository.Table<BranchMediumAccountHead>()
        .FirstOrDefault(b => currentSession != null 
                             && b.AccountCode == bankAccountCode 
                             && b.Session.Id == currentSession.Id);
      var bankAccountAccountHeads = _repository.Table<BranchMediumAccountHead>()
        .Where(w => bankParentAccountHead != null 
                    && w.ParentHead.Id == bankParentAccountHead.Id 
                    && w.Session.Id == message.SessionId 
                    && w.BranchMedium.Id == message.BranchMediumId)
        .ToList();
      var mapper = _mapperConfiguration.CreateMapper();
      var data = mapper.Map<List<BranchMediumAccountsHeadMessageModel>>(bankAccountAccountHeads);
      return data;
    }
  }
}