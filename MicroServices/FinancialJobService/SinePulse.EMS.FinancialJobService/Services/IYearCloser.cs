using System.Threading.Tasks;

namespace SinePulse.EMS.FinancialJobService.Services
{
  public interface IYearCloser
  {
    Task DoYearClosing(long sessionId);
  }
}