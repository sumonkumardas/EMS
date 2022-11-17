using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class AddRoomUseCase : IAddRoomUseCase
  {
    private readonly IRepository _repository;

    public AddRoomUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddRoom(AddRoomRequestMessage requestMessage)
    {
      var branch = _repository.GetById<Branch>(requestMessage.BranchId);
      var room = new Room
      {
        RoomNo = requestMessage.RoomNo,
        ClassTimeCapacity = requestMessage.ClassTimeCapacity,
        ExamTimeCapacity = requestMessage.ExamTimeCapacity,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        Branch = branch
      };

      _repository.Insert(room);
    }
  }
}