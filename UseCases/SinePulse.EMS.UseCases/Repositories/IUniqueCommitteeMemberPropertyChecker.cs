namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueCommitteeMemberPropertyChecker
  {
    bool IsUniqueMobileNo(string mobileNo);
    bool IsUniqueEmailAddress(string emailAddress);
  }
}