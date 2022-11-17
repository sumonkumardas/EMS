using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Repositories;
using AccountHeadTypeEnum = SinePulse.EMS.Messages.Model.Enums.AccountHeadTypeEnum;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAcademicAccountHeadListUseCase : IShowAcademicAccountHeadListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowAcademicAccountHeadListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<AccountHead, AccountHeadMessageModel>());
    }

    public IEnumerable<AccountHeadMessageModel> ShowAcademicAccountHeadList(ShowAcademicAccountHeadListRequestMessage message)
    {
      var accountHeads = _repository.Table<AccountHead>()
        .Include(nameof(AccountHead.ParentHead))
        .Where(a => a.AccountHeadType == Domain.Enums.AccountHeadTypeEnum.Academic).ToList();
      
      var mapper = _autoMapperConfig.CreateMapper();
      var accountHeadMessageModels = mapper.Map(accountHeads, new List<AccountHeadMessageModel>());
      var leafAccountHeads = GetAccountHeadLeafNodes(accountHeadMessageModels);
      return leafAccountHeads.Where(a => (int) a.AccountPeriod == (int) message.AccountPeriod);
    }

    private IEnumerable<AccountHeadMessageModel> GetAccountHeadLeafNodes(List<AccountHeadMessageModel> accountHeads)
    {
      var filteredList = new List<AccountHeadMessageModel>(accountHeads);
      var originalList = accountHeads;

      foreach (var accountHead in accountHeads)
      {
        foreach (var accHead in originalList)
        {
          if (accHead.ParentHead != null && accHead.ParentHead.Id == accountHead.Id)
            filteredList.Remove(accountHead);
        }
      }
      return filteredList;
    }
  }
}