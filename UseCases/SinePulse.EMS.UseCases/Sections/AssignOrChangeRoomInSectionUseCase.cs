using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sections
{
  public class AssignOrChangeRoomInSectionUseCase : IAssignOrChangeRoomInSectionUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AssignOrChangeRoomInSectionUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AssignOrChangeRoomInSection(AssignOrChangeRoomInSectionRequestMessage message)
    {
      var section = _repository.GetById<Section>(message.SectionId);
      var room = _repository.GetById<Room>(message.RoomId);

      section.Room = room;
      _dbContext.SaveChanges();
    }
  }
}