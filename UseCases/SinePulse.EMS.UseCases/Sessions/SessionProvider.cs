using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class SessionProvider : ISessionProvider
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public SessionProvider(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public ICollection<Session> GetBranchMediumSpecificSessions(long branchMediumId)
    {
      var includes = new[] {nameof(BranchMedium.Branch)};
      var branchMedium = _repository.GetByIdWithInclude<BranchMedium>(branchMediumId, includes);
      var branchId = branchMedium.Branch.Id;

      var onlyBranchMediumSpecificSessions = GetOnlyBranchMediumSpecificSessions(branchMediumId);
      var instituteSpecificSessions = GetBranchSpecificSessions(branchId);

      return onlyBranchMediumSpecificSessions.Concat(instituteSpecificSessions).Where(x=>x.EndDate.Ticks>= DateTime.Now.Ticks).ToList();
    }

    public ICollection<Session> GetOnlyBranchMediumSpecificSessions(long branchMediumId)
    {
      return _dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.BranchMedium && s.BranchMedium.Id == branchMediumId).ToList();
    }

    public ICollection<Session> GetBranchSpecificSessions(long branchId)
    {
      var includes = new[] {nameof(Branch.Institute)};
      var branch = _repository.GetByIdWithInclude<Branch>(branchId, includes);
      var instituteId = branch.Institute.Id;

      var onlyBranchSpecificSessions = GetOnlyBranchSpecificSessions(branchId);
      var instituteSpecificSessions = GetInstituteSpecificSessions(instituteId);

      return onlyBranchSpecificSessions.Concat(instituteSpecificSessions).ToList();
    }

    public ICollection<Session> GetOnlyBranchSpecificSessions(long branchId)
    {
      return _dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.Branch && s.Branch.Id == branchId).ToList();
    }

    public ICollection<Session> GetInstituteSpecificSessions(long instituteId)
    {
      var onlyInstituteSpecificSessions = GetOnlyInstituteSpecificSessions(instituteId);
      var globalSessions = GetGlobalSessions();

      return onlyInstituteSpecificSessions.Concat(globalSessions).ToList();
    }

    public ICollection<Session> GetOnlyInstituteSpecificSessions(long instituteId)
    {
      return _dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.Institute && s.Institute.Id == instituteId).ToList();
    }

    public ICollection<Session> GetGlobalSessions()
    {
      return _dbContext.Sessions.Where(s => s.SessionType == ObjectTypeEnum.Global).ToList();
    }
  }
}