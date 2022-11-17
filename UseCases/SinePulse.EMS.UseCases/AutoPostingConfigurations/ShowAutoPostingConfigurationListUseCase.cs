using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AutoPostingConfigurationMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.AutoPostingConfigurations
{
  public class ShowAutoPostingConfigurationListUseCase : IShowAutoPostingConfigurationListUseCase
  {
    private readonly IRepository _repository;
    private MapperConfiguration _automapperConfig;

    public ShowAutoPostingConfigurationListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<AutoPostingConfiguration, AutoPostingConfigurationMessageModel>());
    }

    public List<AutoPostingConfigurationMessageModel> ShowAutoPostingConfigurationList(ShowAutoPostingConfigurationListRequestMessage message)
    {
      var dbAutoPostingConfigurationList = _repository.Table<AutoPostingConfiguration>()
          .Where(x => x.VoucherType == (VoucherTypeEnum) message.VoucherType).ToList();
      var messageModelAutoPostingConfigurationList = new List<AutoPostingConfigurationMessageModel>();

      var mapper = _automapperConfig.CreateMapper();
      List<AutoPostingConfigurationMessageModel> list = mapper.Map(dbAutoPostingConfigurationList, messageModelAutoPostingConfigurationList);
      return list;
    }
  }
}