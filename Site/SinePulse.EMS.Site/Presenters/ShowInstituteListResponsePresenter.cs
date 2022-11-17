using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Presenters
{

  public class ShowInstituteListResponsePresenter
  {
    private MapperConfiguration _automapperConfig;
    public ShowInstituteListViewModel Handle(ShowInstituteListResponseMessage message, ShowInstituteListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      _automapperConfig = new MapperConfiguration(cfg => {

        cfg.CreateMap<Institute, InstituteViewModel>()
        .ForMember(x => x.BrachList, opt => opt.Ignore());

      });
      viewModel.InstituteList = message.InstituteList;
      var mapper = _automapperConfig.CreateMapper();
      viewModel.InstituteViewModelList = mapper.Map<List<InstituteViewModel>>(message.InstituteList);

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
