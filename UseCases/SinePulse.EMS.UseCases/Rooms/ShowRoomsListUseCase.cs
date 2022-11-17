using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class ShowRoomsListUseCase : IShowRoomsListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowRoomsListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomMessageModel>());
    }

    public List<RoomMessageModel> ShowRoomsList(ShowRoomsListRequestMessage message)
    {
      var dbRooms = _repository.Table<BranchMedium>()
        .Include(nameof(BranchMedium.Branch) + "." + nameof(Rooms))
        .Include(nameof(BranchMedium.Medium))
        .Include(nameof(BranchMedium.Sections))
        .FirstOrDefault(brm => brm.Sections.Any(s => s.Id == message.SectionId))
        ?.Branch.Rooms.ToList();
      var mapper = _autoMapperConfig.CreateMapper();
      var roomMessageModel = new List<RoomMessageModel>();
      var list = mapper.Map(dbRooms, roomMessageModel);
      return list;
    }
  }
}