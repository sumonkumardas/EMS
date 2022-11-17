using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowAccountHeadListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public AccountHeadListViewModel Handle(ShowAccountHeadListResponseMessage message, AccountHeadListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {

      _autoMapperConfig = new MapperConfiguration(
          cfg =>
          {
            cfg.CreateMap<AccountTypeMessageModel, AccountTypeViewModel>();
            cfg.CreateMap<AccountHeadMessageModel, AccountHeadViewModel>();
          });
      var mapper = _autoMapperConfig.CreateMapper();

      viewModel.AccountHeads = mapper.Map<List<AccountHeadViewModel>>(message.AccountHeads);
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;
      return viewModel;
    }
  }
}