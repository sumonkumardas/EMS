using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayBranchMediumResponsePresenter
    {
    public BranchMediumViewModel Handle(DisplayBranchMediumResponseMessage message, BranchMediumViewModel viewModel)
    {
      viewModel.Id = message.DisplayBranchMedium.BranchMediumId;
      viewModel.Branch = new BranchViewModel()
      {
        BranchName = message.DisplayModelBranch.BranchName,
        Id = message.DisplayModelBranch.BranchId,
        Institute = new InstituteViewModel()
        {
          Id = message.DisplayModelInstitute.InstituteId,
          OrganisationName = message.DisplayModelInstitute.InstituteName
        }
      };

      return viewModel;
    }
  }
}
