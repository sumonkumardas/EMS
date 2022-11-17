using AutoMapper;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class ShowRoomUseCase : IShowRoomUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowRoomUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomMessageModel>());
    }

    public RoomMessageModel ShowRoom(ShowRoomRequestMessage request)
    {
      var includes = new[] {nameof(Room.Branch)};
      var dbRoom = _repository.GetByIdWithInclude<Room>(request.RoomId, includes);
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map<RoomMessageModel>(dbRoom);
    }
  }
}