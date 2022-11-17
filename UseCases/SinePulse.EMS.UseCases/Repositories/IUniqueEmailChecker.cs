namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueEmailChecker
  {
    bool IsUniqueEmail(string email);
    bool IsUniqueInstitueEmail(string email,long id);
    bool IsUniqueBranchEmail(string email, long id);
    bool IsUniqueContactPersonEmail(string email);
    bool IsUniquePhoneNo(string phoneNo);
  }
}