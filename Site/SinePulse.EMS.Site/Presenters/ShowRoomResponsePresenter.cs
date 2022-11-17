using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowRoomResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;
    public RoomViewModel Handle(ShowRoomResponseMessage message, RoomViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Messages.Model.Examinations.RoomMessageModel, RoomViewModel>());
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel =  mapper.Map<RoomViewModel>(message.Room);

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