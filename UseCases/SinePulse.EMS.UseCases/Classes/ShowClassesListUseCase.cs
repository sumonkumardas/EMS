using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Classes
{
  public class ShowClassesListUseCase : IShowClassesListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowClassesListUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Class, ClassMessageModel>());
    }

    public IEnumerable<ClassMessageModel> GetClasses(ShowClassesListRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      var classes = _repository.Table<Class>().ToList();
      return mapper.Map(classes, new List<ClassMessageModel>());
    }
  }
}