using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowNotificationConfigurationResponsePresenter
  {
    public NotificationConfigurationViewModel Handle(ShowNotificationConfigurationResponseMessage message, NotificationConfigurationViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      if (message.NotificationConfigurationModel != null && message.NotificationConfigurationModel.BranchMedium!=null)
      {
        viewModel.ConfigurationId = message.NotificationConfigurationModel.Id;
        viewModel.EntryDelayPeriod = message.NotificationConfigurationModel.EntryDelayPeriod;
        viewModel.EntryDelayTimeIntervalType = message.NotificationConfigurationModel.EntryDelayTimeIntervalType;
        viewModel.AbsentPeriod = message.NotificationConfigurationModel.AbsentPeriod;
        viewModel.AbsentTimeIntervalType = message.NotificationConfigurationModel.AbsentTimeIntervalType;
        viewModel.ExamStartPeriod = message.NotificationConfigurationModel.ExamStartPeriod;
        viewModel.ExamStartTimeIntervalType = message.NotificationConfigurationModel.ExamStartTimeIntervalType;
        viewModel.ClassTestStartPeriod = message.NotificationConfigurationModel.ClassTestStartPeriod;
        viewModel.ClassTestStartTimeIntervalType = message.NotificationConfigurationModel.ClassTestStartTimeIntervalType;
        viewModel.TermTestStartPeriod = message.NotificationConfigurationModel.TermTestStartPeriod;
        viewModel.TermTestStartTimeIntervalType = message.NotificationConfigurationModel.TermTestStartTimeIntervalType;
        viewModel.ResultGradePreparePeriod = message.NotificationConfigurationModel.ResultGradePreparePeriod;
        viewModel.ResultGradePrepareTimeIntervalType = message.NotificationConfigurationModel.ResultGradePrepareTimeIntervalType;
        viewModel.BranchMediumId = message.NotificationConfigurationModel.BranchMedium.Id;
        viewModel.BranchMediumName = message.NotificationConfigurationModel.BranchMedium.BranchMediumName;
        viewModel.Status = message.NotificationConfigurationModel.Status;
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