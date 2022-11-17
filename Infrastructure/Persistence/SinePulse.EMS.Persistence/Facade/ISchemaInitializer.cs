using Microsoft.EntityFrameworkCore;

namespace SinePulse.EMS.Persistence.Facade
{
  public interface ISchemaInitializer
  {
    void InitializeSchema(ModelBuilder modelBuilder);
  }
}