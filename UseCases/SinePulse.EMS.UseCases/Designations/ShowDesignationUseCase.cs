using AutoMapper;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Designations
{
  public class ShowDesignationUseCase : IShowDesignationUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowDesignationUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Designation, DesignationMessageModel>());
    }

    public DesignationMessageModel ShowDesignation(ShowDesignationRequestMessage message)
    {
      var designation = _repository.GetById<Designation>(message.DesignationId);
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map<DesignationMessageModel>(designation);
    }
  }
}