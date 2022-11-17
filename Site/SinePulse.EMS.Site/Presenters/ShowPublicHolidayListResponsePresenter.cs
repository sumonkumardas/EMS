using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowPublicHolidayListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public PublicHolidayListViewModel Handle(ShowPublicHolidayListResponseMessage message, PublicHolidayListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<PublicHolidayMessageModel, PublicHolidayViewModel>(); });
      var mapper = _autoMapperConfig.CreateMapper();

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.Holidays = mapper.Map<List<PublicHolidayViewModel>>(message.PublicHolidayList);
      return viewModel;
    }
  }
}