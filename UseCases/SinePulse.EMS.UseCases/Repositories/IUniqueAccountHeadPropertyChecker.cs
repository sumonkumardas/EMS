namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueAccountHeadPropertyChecker
  {
    bool IsUniqueAccountCode(string accountCode);
    bool IsUniqueAccountHeadName(string accountHeadName);
    bool IsUniqueAccountHeadName(string accountHeadName, long accountHeadId);
    bool IsUniqueAccountCode(string accountCode, long accountHeadId);

    bool IsUniqueAccountCodeOfBranch(string accountCode,long branchMediumId,long sessionId);
    bool IsUniqueAccountHeadNameOfBranch(string accountHeadName, long branchMediumId, long sessionId);
    }
}