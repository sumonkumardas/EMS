using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowAccountHeadResponsePresenter
  {
    public AccountHeadViewModel Handle(ShowAccountHeadResponseMessage message, AccountHeadViewModel viewModel,
       GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.AccountHeadId = message.AccountHead.Id;
      viewModel.AccountHeadName = message.AccountHead.AccountHeadName;
      viewModel.AccountCode = message.AccountHead.AccountCode;
      viewModel.AccountHeadType = message.AccountHead.AccountHeadType;
      viewModel.AccountPeriod = message.AccountHead.AccountPeriod;
      viewModel.ParentHead = message.AccountHead.ParentHead;
      if (message.AccountHead.ParentHead != null) 
        viewModel.ParentHeadId = message.AccountHead.ParentHead.Id;
      viewModel.Status = message.AccountHead.Status;

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