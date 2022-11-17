using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowDesignationListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public DesignationListViewModel Handle(ShowDesignationListResponseMessage message,
      DesignationListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(
        cfg => cfg.CreateMap<DesignationMessageModel, DesignationViewModel>()
      );
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel.Designations = mapper.Map<List<DesignationViewModel>>(message.DesignationList);

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