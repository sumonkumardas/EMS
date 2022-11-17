using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using System.Collections.Generic;
using System.Linq;
using System;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.Site.Presenters
{
  public class AccountDisplayResponsePresenter
  {
    public AccountDisplayModel Handle(DisplayBranchMediumResponseMessage message,
        AccountDisplayModel viewModel,
        GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.AccountDisplayBranchMedium = new AccountDisplayModel.BranchMedium()
      {
        BranchMediumId = message.DisplayBranchMedium.BranchMediumId,
        BranchMediumName = message.DisplayBranchMedium.BranchMediumName,
        ShiftName = message.DisplayBranchMedium.ShiftName
      };
      viewModel.AccountDisplayBranch = new AccountDisplayModel.Branch()
      {
        BranchId = message.DisplayModelBranch.BranchId,
        BranchName = message.DisplayModelBranch.BranchName
      };
      viewModel.AccountDisplayInstitute = new AccountDisplayModel.Institute()
      {
        InstituteName = message.DisplayModelInstitute.InstituteName,
        InstituteId = message.DisplayModelInstitute.InstituteId
      };

      viewModel.Sessions = new List<AccountDisplayModel.Session>();
      
      foreach (var messageSession in message.Sessions)
      {
        viewModel.Sessions.Add(new AccountDisplayModel.Session()
        {
          SessionId = messageSession.SessionId,
          SessionName = messageSession.SessionName
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

    private static void IterateSession(ICollection<Session> sessions, ref AccountDisplayModel viewModel)
    {
      if (sessions != null && sessions.Count > 0)
      {
        foreach (var branchMediumSession in sessions)
        {
          var session = new AccountDisplayModel.Session()
          {
            EndDate = branchMediumSession.EndDate,
            StartDate = branchMediumSession.StartDate,
            SessionId = branchMediumSession.Id,
            SessionName = branchMediumSession.SessionName
          };

          viewModel.Sessions.Add(session);
        }
      }
    }
    private static void IterateSession(ICollection<SessionMessageModel> sessions, ref AccountDisplayModel viewModel)
    {
      if (sessions != null && sessions.Count > 0)
      {
        foreach (var branchMediumSession in sessions)
        {
          var session = new AccountDisplayModel.Session()
          {
            EndDate = branchMediumSession.EndDate,
            StartDate = branchMediumSession.StartDate,
            SessionId = branchMediumSession.Id,
            SessionName = branchMediumSession.SessionName
          };

          viewModel.Sessions.Add(session);
        }
      }
    }

    private AccountDisplayModel.Session GetCurrentSessionObject(ICollection<Session> branchMediumSessions,
        ICollection<Session> branchSessions, ICollection<SessionMessageModel> instituteSessions)
    {
      if (branchMediumSessions != null && branchMediumSessions.Any())
      {
        var session = branchMediumSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        return new AccountDisplayModel.Session()
        {
          EndDate = session.EndDate,
          StartDate = session.StartDate,
          SessionId = session.Id
        };
      }
      if (branchSessions != null && branchSessions.Any())
      {
        var session = branchSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        return new AccountDisplayModel.Session()
        {
          EndDate = session.EndDate,
          StartDate = session.StartDate,
          SessionId = session.Id
        };
      }
      if (instituteSessions != null && instituteSessions.Any())
      {
        var session = instituteSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        if (session != null)
          return new AccountDisplayModel.Session()
          {
            EndDate = session.EndDate,
            StartDate = session.StartDate,
            SessionId = session.Id
          };
      }
      return null;
    }
  }
}