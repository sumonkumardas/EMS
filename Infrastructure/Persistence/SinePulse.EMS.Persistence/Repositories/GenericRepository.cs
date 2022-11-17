using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.Persistence.Repositories
{
  public class GenericRepository : IRepository
  {
    #region Fields

    private readonly EmsDbContext _dbContext;

    #endregion

    public GenericRepository(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }


    #region Methods

    public virtual TEntity GetById<TEntity>(long id) where TEntity : BaseEntity
    {
      return _dbContext.Find<TEntity>(id);
    }

    public virtual TEntity GetById<TEntity>(
      long id,
      IEnumerable<string> includes)
      where TEntity : BaseEntity
    {
      var entities = _dbContext.Set<TEntity>();
      var query = entities.AsQueryable();
      foreach (var include in includes)
        query = query.Include(include);

      return query.SingleOrDefault(x => x.Id == id);
    }
    
    public virtual TEntity GetById<TEntity, TProperty>(
      long id,
      Expression<Func<TEntity, TProperty>> include)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8, TProperty9>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8,
      Expression<Func<TEntity, TProperty9>> include9)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8)
        .Include(include9);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8, TProperty9, TProperty10>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8,
      Expression<Func<TEntity, TProperty9>> include9,
      Expression<Func<TEntity, TProperty10>> include10)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8)
        .Include(include9)
        .Include(include10);

      return query.SingleOrDefault(x => x.Id == id);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity>(
      long id,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      return await _dbContext.FindAsync<TEntity>(new object[] {id}, cancellationToken: cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity>(
      long id,
      IEnumerable<string> includes,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var entities = _dbContext.Set<TEntity>();
      var query = entities.AsQueryable();
      foreach (var include in includes)
        query = query.Include(include);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty>(
      long id,
      Expression<Func<TEntity, TProperty>> include,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity>
      GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(
        long id,
        Expression<Func<TEntity, TProperty1>> include1,
        Expression<Func<TEntity, TProperty2>> include2,
        Expression<Func<TEntity, TProperty3>> include3,
        Expression<Func<TEntity, TProperty4>> include4,
        Expression<Func<TEntity, TProperty5>> include5,
        CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8, TProperty9>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8,
      Expression<Func<TEntity, TProperty9>> include9,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8)
        .Include(include9);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8, TProperty9, TProperty10>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8,
      Expression<Func<TEntity, TProperty9>> include9,
      Expression<Func<TEntity, TProperty10>> include10,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8)
        .Include(include9)
        .Include(include10);

      return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public virtual TEntity GetByIdWithInclude<TEntity>(long id, string[] includes) where TEntity : BaseEntity
    {
      var entities = _dbContext.Set<TEntity>();

      var query = entities.AsQueryable();
      foreach (var include in includes)
        query = query.Include(include);
      return query.SingleOrDefault(s => s.Id == id); 
    }

    public virtual async Task<TEntity> GetByIdWithIncludeAsync<TEntity>(
      long id,
      string[] includes,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      var entities = _dbContext.Set<TEntity>();

      var query = entities.AsQueryable();
      foreach (var include in includes)
        query = query.Include(include);
      return await query.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public virtual void Insert<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
      try
      {
        if (entity == null)
          throw new ArgumentNullException(nameof(entity));

        _dbContext.Add(entity);
      }
      catch (Exception dbEx)
      {
        throw dbEx;
      }
    }

    public virtual void Insert<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
    {
      try
      {
        if (entities == null)
          throw new ArgumentNullException(nameof(entities));

        foreach (var entity in entities)
        {
          _dbContext.Add(entity);
        }        
      }
      catch (Exception dbEx)
      {
        throw dbEx;
      }
    }

    public virtual async Task InsertAsync<TEntity>(
      TEntity entity,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      if (entity == null)
        throw new ArgumentNullException(nameof(entity));

      await _dbContext.AddAsync(entity, cancellationToken);
    }

    public virtual async Task InsertAsync<TEntity>(
      IEnumerable<TEntity> entities,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
      if (entities == null)
        throw new ArgumentNullException(nameof(entities));

      foreach (var entity in entities)
      {
        await _dbContext.AddAsync(entity, cancellationToken);
      }
    }

    public virtual void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
      try
      {
        if (entity == null)
          throw new ArgumentNullException(nameof(entity));

        _dbContext.Remove(entity);
      }
      catch (Exception dbEx)
      {
        throw dbEx;
      }
    }

    public virtual void Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
    {
      try
      {
        if (entities == null)
          throw new ArgumentNullException(nameof(entities));

        foreach (var entity in entities)
        {
          _dbContext.Remove(entity);
        }
      }
      catch (Exception dbEx)
      {
        throw dbEx;
      }
    }


    #endregion

    #region Properties


    IQueryable<TEntity> IRepository.Table<TEntity>()
    {
      return Entities<TEntity>();
    }

    IQueryable<TEntity> IRepository.Table<TEntity>(string[] includes)
    {
      var entities = _dbContext.Set<TEntity>();

      var query = entities.AsQueryable();
      foreach (var include in includes)
        query = query.Include(include);
      
      return query;
    }

    IQueryable<TEntity> IRepository.TableNoTracking<TEntity>()
    {
      return Entities<TEntity>().AsNoTracking();
    }

    protected virtual DbSet<TEntity> Entities<TEntity>() where TEntity : BaseEntity
    {
      return _dbContext.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> condition)
      where TEntity : BaseEntity
    {
      return _dbContext.Set<TEntity>().Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity>(
      Expression<Func<TEntity, bool>> condition,
      IEnumerable<string> includes)
      where TEntity : BaseEntity
    {
      var entities = _dbContext.Set<TEntity>();
      var query = entities.AsQueryable();
      foreach (var include in includes)
        query = query.Include(include);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty>> include)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8, TProperty9>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8,
      Expression<Func<TEntity, TProperty9>> include9)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8)
        .Include(include9);

      return query.Where(condition);
    }

    public virtual IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7, TProperty8, TProperty9, TProperty10>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7,
      Expression<Func<TEntity, TProperty8>> include8,
      Expression<Func<TEntity, TProperty9>> include9,
      Expression<Func<TEntity, TProperty10>> include10)
      where TEntity : BaseEntity
    {
      var query = _dbContext.Set<TEntity>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .Include(include4)
        .Include(include5)
        .Include(include6)
        .Include(include7)
        .Include(include8)
        .Include(include9)
        .Include(include10);

      return query.Where(condition);
    }

    #endregion
  }
}
