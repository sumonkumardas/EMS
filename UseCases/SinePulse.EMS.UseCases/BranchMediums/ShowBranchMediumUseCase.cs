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
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class ShowBranchMediumUseCase : IShowBranchMediumUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfigurationInstitute;
    private readonly MapperConfiguration _mapperConfigurationSessions;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public ShowBranchMediumUseCase(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
      _mapperConfigurationInstitute = new MapperConfiguration(c => c.CreateMap<Institute, InstituteMessageModel>());
      _mapperConfigurationSessions = new MapperConfiguration(c => c.CreateMap<Session, SessionMessageModel>());
    }

    public BranchMedium ShowBranchMedium(ShowBranchMediumRequestMessage request)
    {
      var sectionClassInclude = nameof(BranchMedium.Sections) + "." + nameof(Class);
      var includes = new[]
      {
        nameof(Branch),
        nameof(BranchMedium.Branch) + "." + nameof(Institute),
        nameof(Shift),
        nameof(Medium),
        nameof(BranchMedium.Sections),
        nameof(BranchMedium.Sections)+"."+nameof(Section.Session),
        nameof(BranchMedium.Sessions),
        sectionClassInclude
      };
      var branchMedium = _repository.GetByIdWithInclude<BranchMedium>(request.BranchMediumId, includes);

      Session currentSession = _currentSessionProvider.GetCurrentSession(request.BranchMediumId);

      var institute = _repository.Table<Institute>()
        .Include(nameof(Institute.Branches))
        .Include(nameof(Institute.Sessions))
        .FirstOrDefault(x => x.Branches.Any(br => br.Id == branchMedium.Branch.Id));

      var sessionMapper = _mapperConfigurationSessions.CreateMapper();
      var instituteMapper = _mapperConfigurationInstitute.CreateMapper();

      var instituteSessions = branchMedium?.Sessions?.Where(s => s.EndDate > DateTime.Now).ToList();
      request.InstituteSessions = sessionMapper.Map(instituteSessions, new List<SessionMessageModel>());

      request.Institute = instituteMapper.Map(institute, new InstituteMessageModel());
      request.CurrentSession = instituteMapper.Map(currentSession, new SessionMessageModel());
      if (currentSession != null)
      {
        branchMedium.Sections = branchMedium.Sections.Where(x => x.Session.Id == currentSession.Id).ToList();
      }

      return branchMedium;
    }
  }
}