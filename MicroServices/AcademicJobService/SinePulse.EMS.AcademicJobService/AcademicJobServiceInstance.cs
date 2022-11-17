using System.Threading.Tasks;

namespace SinePulse.EMS.AcademicJobService
{
  public abstract class AcademicJobServiceInstance
  {
    public abstract Task Start();

    public abstract Task Stop();
  }
}