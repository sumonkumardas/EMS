using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Persistence.Facade
{
  public interface IRoleTableInitializer
  {
    void InitializeRoleTable(AuditFields auditFields);
  }
}