using AutoMapper;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.AuthorizationMessages;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowPublicHolidayResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public PublicHolidayViewModel Handle(ShowPublicHolidayResponseMessage message, PublicHolidayViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<PublicHolidayMessageModel, PublicHolidayViewModel>(); });
      var mapper = _autoMapperConfig.CreateMapper();
      
      viewModel = mapper.Map<PublicHolidayViewModel>(message.PublicHoliday);
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