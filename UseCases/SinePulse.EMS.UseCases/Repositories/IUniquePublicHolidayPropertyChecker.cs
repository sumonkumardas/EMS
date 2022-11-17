namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniquePublicHolidayPropertyChecker
  {
    bool IsUniquePublicHolidayName(string holidayName, int year);
    bool IsUniquePublicHolidayName(string holidayName, int year,long publicHolidayId);
  }
}