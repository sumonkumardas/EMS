using System.Collections.Generic;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowServiceChargeListResponsePresenter
  {
    public ShowServiceChargeListViewModel Handle(ShowServiceChargeListResponseMessage message, ShowServiceChargeListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.serviceCharges = new List<ServiceChargeViewModel>();

      foreach (var ServiceCharge in message.ServiceChargeData)
      {
        var model = new ServiceChargeViewModel
        {
          Id = ServiceCharge.Id,
          PaymentBufferPeriod = ServiceCharge.PaymentBufferPeriod,
          PerStudentCharge = ServiceCharge.PerStudentCharge,
          EffectiveDate = ServiceCharge.EffectiveDate,
          EndDate = ServiceCharge.EndDate,
          InstituteName = ServiceCharge.InstituteName,
          BranchName = ServiceCharge.BranchName,
          BranchMediumData = new ServiceChargeViewModel.BranchMedium {
            BranchMediumId = ServiceCharge.BranchMedium.BranchMediumId, BranchMediumName = ServiceCharge.BranchMedium.BranchMediumName
          },
        };

        viewModel.serviceCharges.Add(model);
      }


      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return viewModel;
    }
  }
}
