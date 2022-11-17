namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueMediumPropertyChecker
  {
    bool IsUniqueMediumName(string mediumName);
    bool IsUniqueMediumCode(string mediumCode);
    bool IsUniqueMediumName(string mediumName, long mediumId);
    bool IsUniqueMediumCode(string mediumCode, long mediumId);
  }
}