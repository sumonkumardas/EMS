using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Branches
{
  public class ShowBranchUseCase : IShowBranchUseCase
  {
    private readonly IRepository _repository;

    public ShowBranchUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Branch ShowBranch(ShowBranchRequestMessage request)
    {
      var branchMediumName = (nameof(Branch.Mediums) + "." + nameof(Medium));
      var includes = new[]
      {
        nameof(Institute), 
        nameof(Branch.Mediums), 
        branchMediumName, 
        nameof(Branch.Sessions), 
        nameof(Branch.Shifts),
        nameof(Branch.Rooms),
        nameof(Branch.Employees)
      };
      var branch = _repository.GetByIdWithInclude<Branch>(request.BranchId, includes);
      var employees = _repository.Table<Domain.Employees.Employee>()
        .Where(e => e.Branch.Id == branch.Id).ToList();
      branch.Employees = employees;
      var instituteSessions = _repository.Table<Institute>()
        .Include(nameof(Institute.Branches))
        .Include(nameof(Institute.Sessions))
        .FirstOrDefault(x => x.Branches.Any(br => br.Id == request.BranchId))
        ?.Sessions.Where(s => s.EndDate > DateTime.Now).ToList();
      request.InstituteSession = instituteSessions;
      return branch;
    }
  }
}