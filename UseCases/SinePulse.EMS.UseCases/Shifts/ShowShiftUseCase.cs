using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class ShowShiftUseCase : IShowShiftUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowShiftUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Shift, ShiftMessageModel>());
    }

    public ShiftMessageModel ShowShift(ShowShiftRequestMessage requestMessage)
    {
      var includes = new[] {nameof(Branch)};
      var shift = _repository.GetByIdWithInclude<Shift>(requestMessage.ShiftId, includes);
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map<ShiftMessageModel>(shift);
    }
  }
}