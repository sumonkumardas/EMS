using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowSessionResponsePresenter
  {
    public SessionViewModel Handle(ViewSessionResponseMessage message, SessionViewModel viewModel, 
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.SessionName = message.Session.SessionName;
      viewModel.Id = message.Session.Id;
      viewModel.StartDate = message.Session.StartDate;
      viewModel.EndDate = message.Session.EndDate;
      viewModel.Status = message.Session.Status;
      viewModel.IsSessionClosed = message.Session.IsSessionClosed;
      viewModel.SessionType = message.Session.SessionType;
      viewModel.ObjectId = GetObjectId(message);
      viewModel.Institute = message.Session.Institute;

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

    private static long GetObjectId(ViewSessionResponseMessage message)
    {
      switch (message.Session.SessionType)
      {
        case ObjectTypeEnum.Global:
          return 0;
        case ObjectTypeEnum.Institute:
          return message.Session.Institute.Id;
        case ObjectTypeEnum.Branch:
          return message.Session.Branch.Id;
        case ObjectTypeEnum.Medium:
          return message.Session.BranchMedium.Id;
        case ObjectTypeEnum.BranchMedium:
          return message.Session.BranchMedium.Id;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}