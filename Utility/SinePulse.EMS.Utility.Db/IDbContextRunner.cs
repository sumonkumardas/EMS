using System;
using Microsoft.EntityFrameworkCore;

namespace SinePulse.EMS.Utility.Db
{
  public interface IDbContextRunner
  {
    void RunInDbContext<TDbContext>(Action<TDbContext> runAction)
      where TDbContext : DbContext, new();

    TResult RunInDbContext<TDbContext, TResult>(Func<TDbContext, TResult> runFunction)
      where TDbContext : DbContext, new();

    void RunAndSaveInDbContext<TDbContext>(Action<TDbContext> runAction)
      where TDbContext : DbContext, new();

    TResult RunAndSaveInDbContext<TDbContext, TResult>(Func<TDbContext, TResult> runFunction)
      where TDbContext : DbContext, new();
  }
}