namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueLeaveTypePropertyChecker
  {
    bool IsUniqueLeaveName(string leaveName);
    bool IsUniqueLeaveName(string leaveName,long leaveId);
  }
}