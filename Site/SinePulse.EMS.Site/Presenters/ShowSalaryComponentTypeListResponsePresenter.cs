using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowSalaryComponentTypeListResponsePresenter
  {
    public ShowSalaryComponentTypeListViewModel Handle(ShowSalaryComponentTypeListResponseMessage message, ShowSalaryComponentTypeListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.salaryComponentTypes = new List<SalaryComponentTypeViewModel>();

      foreach (var SalaryComponentType in message.SalaryComponentTypeData)
      {
        var model = new SalaryComponentTypeViewModel
        {
          Id = SalaryComponentType.Id,
          ComponentType = SalaryComponentType.ComponentType,
          Status = SalaryComponentType.Status
        };

        viewModel.salaryComponentTypes.Add(model);
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
