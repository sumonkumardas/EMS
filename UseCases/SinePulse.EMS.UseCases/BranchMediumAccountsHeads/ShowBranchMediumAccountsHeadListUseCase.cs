using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class ShowBranchMediumAccountsHeadListUseCase : IShowBranchMediumAccountsHeadListUseCase
  {
    private readonly IRepository _repository;
    private MapperConfiguration _automapperConfig;

    public ShowBranchMediumAccountsHeadListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<BranchMediumAccountHead, BranchMediumAccountsHeadMessageModel>()
      .ForMember(x => x.BranchMedium, opt => opt.Ignore()));
    }

    public List<BranchMediumAccountsHeadMessageModel> ShowBranchMediumAccountsHeadList(ShowBranchMediumAccountsHeadListRequestMessage message)
    {
      var includes = new string[] { nameof(BranchMediumAccountHead.BranchMedium),nameof(BranchMediumAccountHead.Session), nameof(BranchMediumAccountHead.AccountType), nameof(BranchMediumAccountHead.ParentHead) };
      
      var dbBranchMediumAccountsHeadList = _repository.Table<BranchMediumAccountHead>(includes).ToList().Where(x=>x.Session.Id == message.CurrentSessionId && x.BranchMedium.Id == message.BranchMediumId);
      

      if (!string.IsNullOrEmpty(message.ParentAccountHeadCode))
          dbBranchMediumAccountsHeadList = dbBranchMediumAccountsHeadList
              .Where(x =>x.ParentHead != null &&  x.ParentHead.AccountCode == message.ParentAccountHeadCode).ToList();
      var messageModelBranchMediumAccountsHeadList = new List<BranchMediumAccountsHeadMessageModel>();

      var mapper = _automapperConfig.CreateMapper();
      List<BranchMediumAccountsHeadMessageModel> list = mapper.Map(dbBranchMediumAccountsHeadList, messageModelBranchMediumAccountsHeadList);
      return list;
    }
  }
}