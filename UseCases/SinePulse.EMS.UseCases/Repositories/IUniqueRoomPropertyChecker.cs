namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueRoomPropertyChecker
  {
    bool IsUniqueRoomNo(string roomNo, long branchId);
    bool IsUniqueRoomNo(string roomNo, long branchId, long roomId);
  }
}