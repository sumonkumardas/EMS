using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class ShowMediumListUseCase : IShowMediumListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowMediumListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Medium, MediumMessageModel>());
    }

    public IEnumerable<MediumMessageModel> ShowMediumList(ShowMediumListRequestMessage message)
    {
      var mapper = _autoMapperConfig.CreateMapper();
      if (message.BranchId > 0)
      {
        var mediumsOfBranch = _repository.Table<BranchMedium>()
          .Include(nameof(BranchMedium.Medium))
          .Include(nameof(BranchMedium.Branch))
          .Where(brm => brm.Branch.Id == message.BranchId)
          .Select(brm => brm.Medium); 
        return mapper.Map(mediumsOfBranch, new List<MediumMessageModel>());
      }
      var mediums = _repository.Table<Medium>().ToList();
      return mapper.Map(mediums, new List<MediumMessageModel>());
    }
  }
}