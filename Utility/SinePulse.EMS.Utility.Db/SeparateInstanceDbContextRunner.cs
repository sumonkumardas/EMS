using System;
using Microsoft.EntityFrameworkCore;

namespace SinePulse.EMS.Utility.Db
{
  public class SeparateInstanceDbContextRunner : IDbContextRunner
  {
    public void RunInDbContext<TDbContext>(Action<TDbContext> runAction)
      where TDbContext : DbContext, new()
    {
      using (var dbContext = new TDbContext())
      {
        runAction(dbContext);
      }
    }

    public TResult RunInDbContext<TDbContext, TResult>(Func<TDbContext, TResult> runFunction)
      where TDbContext : DbContext, new()
    {
      using (var dbContext = new TDbContext())
      {
        return runFunction(dbContext);
      }
    }

    public void RunAndSaveInDbContext<TDbContext>(Action<TDbContext> runAction)
      where TDbContext : DbContext, new()
    {
      using (var dbContext = new TDbContext())
      {
        runAction(dbContext);
        dbContext.SaveChanges();
      }
    }

    public TResult RunAndSaveInDbContext<TDbContext, TResult>(Func<TDbContext, TResult> runFunction)
      where TDbContext : DbContext, new()
    {
      using (var dbContext = new TDbContext())
      {
        var result = runFunction(dbContext);
        dbContext.SaveChanges();
        return result;
      }
    }
  }
}