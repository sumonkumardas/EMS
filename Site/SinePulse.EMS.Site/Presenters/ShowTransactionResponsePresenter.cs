using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Transactions;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowTransactionResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public TransactionViewModel Handle(ShowTransactionResponseMessage message, TransactionViewModel viewModel)
    {
        viewModel.TransactionEntries = new List<TransactionEntryViewModel>();
      _autoMapperConfig = new MapperConfiguration(
          cfg =>
          {
              cfg.CreateMap<BranchMediumAccountsHeadMessageModel, BranchMediumAccountsHeadViewModel>()
                  .ForMember(x => x.AssociatedWith, opt => opt.Ignore())
                  .ForMember(x => x.LoginUsersBranchId, opt => opt.Ignore())
                  .ForMember(x => x.LoginUsersBranchMediumId, opt => opt.Ignore())
                  .ForMember(x => x.LoginUsersInstituteId, opt => opt.Ignore())
                  .ForMember(x => x.LoginName, opt => opt.Ignore())
                  .ForMember(x => x.RoleFeatures, opt => opt.Ignore());
              cfg.CreateMap<BranchMediumMessageModel, BranchMediumViewModel>();
              cfg.CreateMap<TransactionEntryMessageModel, TransactionEntryViewModel>()
                  .ForMember(x => x.CreditAmount, opt => opt.Ignore())
                  .ForMember(x => x.DebitAmount, opt => opt.Ignore());
              cfg.CreateMap<TransactionMessageModel, TransactionViewModel>()
                  .ForMember( x=> x.AssociatedWith, opt => opt.Ignore())
                  .ForMember(x => x.LoginUsersBranchId, opt => opt.Ignore())
                  .ForMember(x => x.LoginUsersBranchMediumId, opt => opt.Ignore())
                  .ForMember(x => x.LoginUsersInstituteId, opt => opt.Ignore())
                  .ForMember(x => x.LoginName, opt => opt.Ignore())
                  .ForMember(x => x.RoleFeatures, opt => opt.Ignore());
              cfg.CreateMap<AccountTypeMessageModel, AccountTypeViewModel>()
                .ForMember(x => x.AssociatedWith, opt => opt.Ignore())
                .ForMember(x => x.LoginUsersBranchId, opt => opt.Ignore())
                .ForMember(x => x.LoginUsersBranchMediumId, opt => opt.Ignore())
                .ForMember(x => x.LoginUsersInstituteId, opt => opt.Ignore())
                .ForMember(x => x.LoginName, opt => opt.Ignore())
                .ForMember(x => x.RoleFeatures, opt => opt.Ignore());
          });
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel = mapper.Map<TransactionViewModel>(message.Transaction);
      return viewModel;
    }
  }
}