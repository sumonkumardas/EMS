using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Persistence.Facade
{
  public interface IUserTableInitializer
  {
    void InitializeUserTable(AuditFields auditFields);
  }
}