using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAccountHeadListUseCase : IShowAccountHeadListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowAccountHeadListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<AccountHead, AccountHeadMessageModel>());
    }

    public IEnumerable<AccountHeadMessageModel> ShowAccountHeadList(ShowAccountHeadListRequestMessage message)
    {
      var accountHeads = _repository.Table<AccountHead>()
        .Include(nameof(AccountType))
        .Include(nameof(AccountHead.ParentHead))
        .ToList();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(accountHeads, new List<AccountHeadMessageModel>());
    }
  }
}