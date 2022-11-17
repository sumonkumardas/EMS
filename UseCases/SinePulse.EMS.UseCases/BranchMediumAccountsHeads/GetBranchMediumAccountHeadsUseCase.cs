using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class GetBranchMediumAccountHeadsUseCase : IGetBranchMediumAccountHeadsUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetBranchMediumAccountHeadsUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration =
        new MapperConfiguration(c => c.CreateMap<BranchMediumAccountHead, BranchMediumAccountsHeadMessageModel>());
    }

    public IEnumerable<BranchMediumAccountsHeadMessageModel> GetAccountHeads(
      GetBranchMediumAccountHeadsRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      var accountHeads = _repository.Table<BranchMediumAccountHead>()
        .Include(nameof(BranchMediumAccountHead.ParentHead))
        .Include(nameof(BranchMedium))
        .Where(a => a.BranchMedium.Id == message.BranchMediumId &&
                    a.AccountHeadType == AccountHeadTypeEnum.Academic &&
                    a.AccountPeriod == message.AccountPeriod &&
                    a.IsLedger)
        .ToList();
      return mapper.Map(accountHeads, new List<BranchMediumAccountsHeadMessageModel>());
    }
  }
}