using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class ShowBranchMediumAccountHeadUseCase : IShowBranchMediumAccountHeadUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowBranchMediumAccountHeadUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration =
        new MapperConfiguration(c => c.CreateMap<BranchMediumAccountHead, BranchMediumAccountsHeadMessageModel>());
    }

    public BranchMediumAccountsHeadMessageModel GetAccountHead(
      ShowBranchMediumAccountHeadRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      
      var accountHeads = _repository.GetById<BranchMediumAccountHead>(message.AccountHeadId);
      
      return mapper.Map(accountHeads, new BranchMediumAccountsHeadMessageModel());
    }
  }
}