using System;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Persistence.Facade
{
  public class DbInitializer : IDbInitializer
  {
    private readonly IUserTableInitializer _userTableInitializer;
    private readonly IRoleTableInitializer _roleTableInitializer;
    private readonly IChartsOfAccountsInitializer _chartsOfAccountsInitializer;

    public DbInitializer(IRoleTableInitializer roleTableInitializer, IUserTableInitializer userTableInitializer,
      IChartsOfAccountsInitializer chartsOfAccountsInitializer)
    {
      _roleTableInitializer = roleTableInitializer;
      _userTableInitializer = userTableInitializer;
      _chartsOfAccountsInitializer = chartsOfAccountsInitializer;
    }

    public void InitializeDb()
    {
      var auditFields = GetAuditFields("Automatic", DateTime.Now);
      _roleTableInitializer.InitializeRoleTable(auditFields);
      _userTableInitializer.InitializeUserTable(auditFields);
      _chartsOfAccountsInitializer.InitializeChartsOfAccounts();
    }

    private static AuditFields GetAuditFields(string insertedBy, DateTime insertedTimestamp)
    {
      return new AuditFields
      {
        InsertedBy = insertedBy,
        InsertedDateTime = insertedTimestamp,
        LastUpdatedBy = insertedBy,
        LastUpdatedDateTime = insertedTimestamp
      };
    }

  }
}