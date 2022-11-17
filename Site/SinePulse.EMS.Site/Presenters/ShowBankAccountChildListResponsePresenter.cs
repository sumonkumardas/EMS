using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AutoPostingConfigurationMessages;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowBankAccountChildListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public List<BankAccountChildViewModel> Handle(ShowBranchMediumAccountsHeadListResponseMessage message, List<BankAccountChildViewModel> viewModel)
    {
      
      _autoMapperConfig = new MapperConfiguration(
          cfg =>
          {
              cfg.CreateMap<BranchMediumAccountsHeadMessageModel, BankAccountChildViewModel>();
          });
            var mapper = _autoMapperConfig.CreateMapper();
      viewModel = mapper.Map<List<BankAccountChildViewModel>>(message.BranchMediumAccountsHeadList);
      return viewModel;
    }
  }
}