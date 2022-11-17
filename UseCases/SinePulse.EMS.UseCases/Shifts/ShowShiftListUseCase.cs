using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class ShowShiftListUseCase  : IShowShiftListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowShiftListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Shift, ShiftMessageModel>());
    }

    public IEnumerable<ShiftMessageModel> ShowShiftList(ShowShiftListRequestMessage requestMessage)
    {
      var shifts = _repository.Table<Shift>().Where(x=>x.Branch.Id == requestMessage.BranchId).ToList();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(shifts, new List<ShiftMessageModel>());
    }
  }
}