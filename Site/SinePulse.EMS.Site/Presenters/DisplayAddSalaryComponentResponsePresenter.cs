using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayAddSalaryComponentResponsePresenter
  {
    public AddSalaryComponentViewModel Handle(DisplayAddSalaryComponentResponseMessage message, AddSalaryComponentViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      var salaryComponentTypeCollection = new List<AddSalaryComponentViewModel.SalaryComponentType>();
      
      foreach (var SalaryComponent in message.ComponentTypes)
      {
        var model = new AddSalaryComponentViewModel.SalaryComponentType
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
      return viewModel;
    }
  }
}
