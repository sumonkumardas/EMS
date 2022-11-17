using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class DisplayBranchMediumUseCase : IDisplayBranchMediumUseCase
  {
    private readonly IRepository _repository;

    public DisplayBranchMediumUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public BranchMedium ShowBranchMedium(DisplayBranchMediumRequestMessage request)
    {
      var includes = new[]
      {
        nameof(Branch),
        nameof(BranchMedium.Branch) + "." + nameof(Institute),
        nameof(BranchMedium.Medium),
        nameof(BranchMedium.Shift)
      };

      if (request.LoadSessions)
      {
        includes = new[]
        {
          nameof(Branch),
          nameof(BranchMedium.Branch) + "." + nameof(Institute),
          nameof(BranchMedium.Medium),
          nameof(BranchMedium.Shift),
          nameof(BranchMedium.Sessions),
          nameof(BranchMedium.Branch) + "." + nameof(Branch.Sessions),
          nameof(BranchMedium.Branch) + "." + nameof(Branch.Institute)+"."+nameof(Branch.Institute.Sessions),
        };
      }

      var branchMedium = _repository.GetByIdWithInclude<BranchMedium>(request.BranchMediumId, includes);
      
      return branchMedium;
    }
  }
}