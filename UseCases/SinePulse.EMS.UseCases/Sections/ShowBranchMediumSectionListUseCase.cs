using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Repositories;
using System.Linq;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.Sections
{
  public class ShowBranchMediumSectionListUseCase : IShowBranchMediumSectionListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public ShowBranchMediumSectionListUseCase(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Section, SectionMessageModel>());
    }

    public IEnumerable<SectionMessageModel> ShowBranchMediumSectionList(ShowBranchMediumSectionListRequestMessage message)
    {
      var sectionsOfBranchMedium = _repository.Table<Section>()
        .Include(nameof(Section.BranchMedium))
        .Include(nameof(Section.BranchMedium) +"."+ nameof(Medium))
        .Include(nameof(Section.Class))
        .Include(nameof(Section.Session))
        .Include(nameof(Section.Room))
        .Where(s => s.BranchMedium.Medium.Id == message.MediumId 
                    && s.BranchMedium.Branch.Id == message.BranchId);
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(sectionsOfBranchMedium, new List<SectionMessageModel>());
    }
  }
}