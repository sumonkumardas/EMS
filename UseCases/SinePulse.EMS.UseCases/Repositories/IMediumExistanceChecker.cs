namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IMediumExistanceChecker
  {
    bool IsMediumExists(long mediumId);
  }
}