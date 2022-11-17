using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayEditSalaryComponentResponsePresenter
  {
    public EditSalaryComponentViewModel Handle(DisplayEditSalaryComponentResponseMessage message, EditSalaryComponentViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      var salaryComponentTypeCollection = new List<EditSalaryComponentViewModel.SalaryComponentType>();

      foreach (var SalaryComponent in message.ComponentTypes)
      {
        var model = new EditSalaryComponentViewModel.SalaryComponentType
        {
          ComponentTypeId = SalaryComponent.ComponentTypeId,
          ComponentTypeName = SalaryComponent.ComponentTypeName
        };
        salaryComponentTypeCollection.Add(model);
      }

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.SalaryComponentTypes = salaryComponentTypeCollection;
      viewModel.SalaryComponentId = message.SalaryComponentId;
      viewModel.ComponentName = message.ComponentName;
      viewModel.SalaryComponentTypeId = message.ComponentTypeId;
      return viewModel;
    }
  }
}
