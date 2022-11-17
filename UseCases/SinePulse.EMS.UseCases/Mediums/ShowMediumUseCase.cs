using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class ShowMediumUseCase : IShowMediumUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowMediumUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Medium, MediumMessageModel>());
    }

    public MediumMessageModel ShowMedium(ShowMediumRequestMessage message)
    {
      var medium = _repository.GetById<Medium>(message.MediumId);
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map<MediumMessageModel>(medium);
    }
  }
}