using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;
namespace SinePulse.EMS.Site.Presenters
{
  public class ShowTermListResponsePresenter
  {
    public ShowTermListViewModel Handle(ShowTermListResponseMessage message, ShowTermListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
        viewModel.Terms = new List<TermViewModel>();
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
      foreach (var term in message.TermDataList)
        {
          var convertedTerm = new TermViewModel
          {
            TermId = term.Id, TermName = term.TermName, StartDate = term.StartDate, EndDate = term.EndDate,
            SessionData = new TermViewModel.Session()
            {
              SessionText = term.SessionName,
              SessionId = term.SessionId
            }
          };

          viewModel.Terms.Add(convertedTerm);
        }

      return viewModel;
    }

  }
}