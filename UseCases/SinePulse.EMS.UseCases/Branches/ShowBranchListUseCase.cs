using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Branches
{
  public class ShowBranchListUseCase : IShowBranchListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowBranchListUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Branch, BranchMessageModel>());
    }

    public IEnumerable<BranchMessageModel> ShowBranchList(ShowBranchListRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      if (message.InstituteId > 0)
      {
        var branchesOfInstitute = _repository.Table<Branch>()
          .Include(nameof(Branch.Institute))
          .Where(br => br.Institute.Id == message.InstituteId);
        return mapper.Map(branchesOfInstitute, new List<BranchMessageModel>());
      }
      var branches = _repository.Table<Branch>().ToList();
      return mapper.Map(branches, new List<BranchMessageModel>());
    }
  }
}