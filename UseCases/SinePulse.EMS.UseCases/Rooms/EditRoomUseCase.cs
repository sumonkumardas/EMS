using System;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class EditRoomUseCase : IEditRoomUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditRoomUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void EditRoom(EditRoomRequestMessage message)
    {
      var room = _repository.GetById<Room>(message.Id);

      room.RoomNo = message.RoomNo;
      room.ClassTimeCapacity = message.ClassTimeCapacity;
      room.ExamTimeCapacity = message.ExamTimeCapacity;
      room.Status = message.Status;

      room.AuditFields.LastUpdatedBy = message.CurrentUserName;
      room.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
    }
  }
}