using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class ShowBranchMediumListUseCase : IShowBranchMediumListUseCase
  {
    private readonly IRepository _repository;
    private  MapperConfiguration _automapperConfig;

    public ShowBranchMediumListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<BranchMedium, BranchMediumMessageModel>());
    }

    public List<BranchMediumMessageModel> ShowBranchMediumList(ShowBranchMediumListRequestMessage request)
    {
      var mapper = _automapperConfig.CreateMapper();
      if (request.BranchId > 0)
      {
        var branchMediums = _repository.Table<BranchMedium>()
          .Include(nameof(Branch))
          .Include(nameof(Medium))
          .Include(nameof(Shift))
          .Where(brm => brm.Branch.Id == request.BranchId).ToList();
        return mapper.Map(branchMediums, new List<BranchMediumMessageModel>());
      }
      
      var includes = new[]
      {
        nameof(Branch),nameof(Medium)
      };
      var dbBranchMediumList =  _repository.Table<BranchMedium>(includes).ToList();
      var messageModelBranchMediumList = new List<BranchMediumMessageModel>();
      
      List <BranchMediumMessageModel> list = mapper.Map(dbBranchMediumList, messageModelBranchMediumList);
      return list;
      
    }
  }
}