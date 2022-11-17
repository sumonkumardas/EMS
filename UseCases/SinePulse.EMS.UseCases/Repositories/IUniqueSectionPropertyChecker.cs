namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueSectionPropertyChecker
  {
    bool IsUniqueSectionName(string sectionName);
  }
}