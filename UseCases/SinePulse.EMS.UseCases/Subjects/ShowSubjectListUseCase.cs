using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class ShowSubjectListUseCase : IShowSubjectListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowSubjectListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectMessageModel>());
    }

    public IEnumerable<SubjectMessageModel> ShowSubjectList(ShowSubjectListRequestMessage message)
    {
      var subjects = _repository.Table<Subject>().ToList();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(subjects, new List<SubjectMessageModel>());
    }
  }
}