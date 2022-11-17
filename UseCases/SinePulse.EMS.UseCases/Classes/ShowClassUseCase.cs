using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Classes
{
  public class ShowClassUseCase : IShowClassUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowClassUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Class, ClassMessageModel>());
    }

    public ClassMessageModel GetClass(ShowClassRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      var @class= _repository.GetById<Class>(message.ClassId);
      return mapper.Map(@class, new ClassMessageModel());
    }
  }
}