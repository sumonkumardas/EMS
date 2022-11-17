using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowTermResponsePresenter
  {
    public TermViewModel Handle(ShowTermResponseMessage message, TermViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.TermId = message.TermData.Id;
      viewModel.TermName = message.TermData.TermName;
      viewModel.StartDate = message.TermData.StartDate;
      viewModel.EndDate = message.TermData.EndDate;
      viewModel.Status = message.TermData.Status;
      viewModel.BranchMediumData = new TermViewModel.BranchMedium
      {
        Id = message.TermData.BranchMedium.Id,
        InstituteName = message.TermData.BranchMedium.InstituteName,
        BranchName = message.TermData.BranchMedium.BranchName,
        MediumName = message.TermData.BranchMedium.MediumName
      };
      viewModel.SessionData = ToViewModelSession(message.TermData.Session);
      viewModel.ExamConfigurations = message.TermData.ExamTypes;

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

    private static TermViewModel.Session ToViewModelSession(SingleLineSessionResult responseSession)
    {
      return new TermViewModel.Session
      {
        SessionId = responseSession.SessionId,
        SessionText =
          $"{responseSession.SessionName} - [{responseSession.SessionType}: {responseSession.SessionAssociatedObjectName}]"
      };
    }
  }
}