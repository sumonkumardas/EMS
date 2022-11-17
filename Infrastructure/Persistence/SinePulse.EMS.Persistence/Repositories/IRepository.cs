using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Persistence.Repositories
{
  public interface IRepository
  {
    #region Methods

    TEntity GetById<TEntity>(long id) where TEntity : BaseEntity;
    
    TEntity GetById<TEntity>(
      long id,
      IEnumerable<string> includes)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty>(
      long id,
      Expression<Func<TEntity, TProperty>> include)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7)
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    TEntity GetById<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity>(
      long id,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity>(
      long id,
      IEnumerable<string> includes,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty>(
      long id,
      Expression<Func<TEntity, TProperty>> include,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6>(
      long id,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    Task<TEntity> GetByIdAsync<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;
    
    
    TEntity GetByIdWithInclude<TEntity>(long id,string[] includes) where TEntity : BaseEntity;

    Task<TEntity> GetByIdWithIncludeAsync<TEntity>(
      long id,
      string[] includes,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    void Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;
    void Insert<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;

    Task InsertAsync<TEntity>(
      TEntity entity,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    Task InsertAsync<TEntity>(
      IEnumerable<TEntity> entities,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity;

    void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;
    void Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;

    IQueryable<TEntity> Table<TEntity>() where TEntity : BaseEntity;
    IQueryable<TEntity> Table<TEntity>(string[] includes) where TEntity : BaseEntity;
    IQueryable<TEntity> TableNoTracking<TEntity>() where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> condition)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity>(
      Expression<Func<TEntity, bool>> condition,
      IEnumerable<string> includes)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty>> include)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
      TProperty6, TProperty7>(
      Expression<Func<TEntity, bool>> condition,
      Expression<Func<TEntity, TProperty1>> include1,
      Expression<Func<TEntity, TProperty2>> include2,
      Expression<Func<TEntity, TProperty3>> include3,
      Expression<Func<TEntity, TProperty4>> include4,
      Expression<Func<TEntity, TProperty5>> include5,
      Expression<Func<TEntity, TProperty6>> include6,
      Expression<Func<TEntity, TProperty7>> include7)
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;

    IQueryable<TEntity> Filter<TEntity, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5,
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
      where TEntity : BaseEntity;
    
    #endregion
  }
}