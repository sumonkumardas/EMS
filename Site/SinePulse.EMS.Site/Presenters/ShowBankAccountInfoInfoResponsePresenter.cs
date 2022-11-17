using System.Collections.Generic;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowBankAccountInfoInfoResponsePresenter
  {
    public ShowBankAccountInfoListViewModel Handle(ShowBankListResponseMessage message, ShowBankAccountInfoListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.BankAccountInfos = new List<BankAccountInfoViewModel>();
      foreach (var messageBankInfo in message.BankInfos)
      {
        viewModel.BankAccountInfos.Add(new BankAccountInfoViewModel()
        {
          BankId = messageBankInfo.BankId,
          AccountNumber = messageBankInfo.AccountNumber,
          BankName = messageBankInfo.BankName,
          BankBranchId = messageBankInfo.BankBranchId,
          BankBranchName = messageBankInfo.BankBranchName
        });
      }
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