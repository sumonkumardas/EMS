using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueRoomPropertyChecker : IUniqueRoomPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueRoomPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueRoomNo(string roomNo, long branchId)
    {
      return !_dbContext.Rooms.Where(r => r.Branch.Id == branchId).Any(r => r.RoomNo == roomNo);
    }

    public bool IsUniqueRoomNo(string roomNo, long branchId, long roomId)
    {
      return !_dbContext.Rooms.Where(r => r.Branch.Id == branchId).Any(r => r.RoomNo == roomNo && r.Id != roomId);
    }
  }
}