using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAccountHeadUseCase : IShowAccountHeadUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowAccountHeadUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<AccountHead, AccountHeadMessageModel>());
    }

    public AccountHeadMessageModel ShowAccountHead(ShowAccountHeadRequestMessage message)
    {
      var accountHead = _repository.Table<AccountHead>()
        .Include(nameof(AccountHead.ParentHead))
        .FirstOrDefault(a => a.Id == message.AccountHeadId);
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map<AccountHeadMessageModel>(accountHead);
    }
  }
}