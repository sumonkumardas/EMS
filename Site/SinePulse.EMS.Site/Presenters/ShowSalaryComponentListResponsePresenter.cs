using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowSalaryComponentListResponsePresenter
  {
    public ShowSalaryComponentListViewModel Handle(ShowSalaryComponentListResponseMessage message, ShowSalaryComponentListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.salaryComponents = new List<SalaryComponentViewModel>();

      foreach (var salaryComponent in message.SalaryComponentData)
      {
        var model = new SalaryComponentViewModel
        {
          SalaryComponentId = salaryComponent.SalaryComponentId,
          ComponentName = salaryComponent.ComponentName,
          componentTypeData = new SalaryComponentViewModel.SalaryComponentType
          {
            SalaryComponentTypeId = salaryComponent.ComponentType.SalaryComponentTypeId,
            ComponentType = salaryComponent.ComponentType.ComponentType
          }
        };

        viewModel.salaryComponents.Add(model);
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
