namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueInstitutePropertyChecker
  {
    bool IsUniqueInstituteName(string instituteName);
    bool IsUniqueInstituteShortName(string instituteShortName);

    bool IsUniqueInstituteName(string instituteName,long instituteId);
    bool IsUniqueInstituteShortName(string instituteShortName,long instituteId);
  }
}