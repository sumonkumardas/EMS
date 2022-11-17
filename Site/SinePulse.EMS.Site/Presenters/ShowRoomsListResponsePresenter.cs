using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowRoomsListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public List<RoomViewModel> Handle(ShowRoomsListResponseMessage message, List<RoomViewModel> viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<RoomMessageModel, RoomViewModel>(); });
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel = mapper.Map<List<RoomViewModel>>(message.RoomList);

      return viewModel;
    }
  }
}