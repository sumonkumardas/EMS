namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidSectionChecker
  {
    bool IsValidSection(long sectionId);
  }
}